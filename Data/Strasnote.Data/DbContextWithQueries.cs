// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Data
{
	/// <inheritdoc cref="IDbContext{TEntity}"/>
	public abstract class DbContextWithQueries<TEntity> : DbContext<TEntity>, IDbContextWIthQueries<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Database queries
		/// </summary>
		protected IDbQueries Queries { get; private init; }

		/// <summary>
		/// Table name
		/// </summary>
		protected string Table { get; private init; }

		/// <summary>
		/// Inject connection details
		/// </summary>
		/// <param name="client">IDbClient object</param>
		/// <param name="log">ILog (should be created with the context as the class implementing this abstract)</param>
		/// <param name="table">The table name for this entity</param>
		protected DbContextWithQueries(IDbClientWithQueries client, ILog log, string table) : base(client, log) =>
			(Queries, Table) = (client.Queries, table);

		#region Queries

		/// <summary>
		/// Cache matching model columns so expensive reflection isn't done more than once
		/// </summary>
		static private readonly ConcurrentDictionary<Type, List<string>> modelColumnCache = new();

		/// <summary>
		/// Get columns that match between the current entity and <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		private List<string> GetColumns<TModel>() =>
			modelColumnCache.GetOrAdd(typeof(TModel), _ =>
			{
				// Join on property name so only shared properties are returned
				var shared = from e in typeof(TEntity).GetProperties()
							 join m in typeof(TModel).GetProperties() on e.Name equals m.Name
							 where e.GetCustomAttribute<IgnoreAttribute>() == null
							 select e.Name;

				// If any are shared, return as list
				if (shared.Any())
				{
					return shared.ToList();
				}

				// Otherwise select all
				return new() { { Queries.SelectAll } };
			});

		#endregion

		#region Custom Queries

		/// <inheritdoc/>
		public Task<IEnumerable<TModel>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		)
		{
			// Convert the expressions to column names
			var where = new List<(string, SearchOperator, object)>();
			foreach (var (property, op, value) in predicates)
			{
				if (((MemberExpression)property.Body).Member.Name is string name)
				{
					where.Add((name, op, value));
				}
			}

			// Log retrieve
			var (query, param) = Queries.GetRetrieveQuery(Table, GetColumns<TModel>(), where);
			LogOperation(Operation.Retrieve, "{Query} - {@Parameters}", query, param);

			// Perform retrieve and map to TModel
			return Connection.QueryAsync<TModel>(
				sql: query,
				param: param,
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public async Task<TModel> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		) =>
			(await QueryAsync<TModel>(predicates).ConfigureAwait(false)).Single();

		#endregion

		#region Standard CRUD Operations

		/// <inheritdoc/>
		public override Task<TModel> CreateAsync<TModel>(TEntity entity)
		{
			// Log create
			var query = Queries.GetCreateQuery(Table, GetColumns<TModel>());
			LogOperation(Operation.Create, "{Query} {@Entity}", entity, query);

			// Perform create and return created entity
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: entity,
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public override Task<IEnumerable<TModel>> RetrieveAsync<TModel>(string query, object parameters, CommandType commandType)
		{
			// Log retrieve
			LogOperation(Operation.Retrieve, "{CommandType} {Query} - {@Parameters}", commandType, query, parameters);

			// Perform retrieve and map to TModel
			return Connection.QueryAsync<TModel>(
				sql: query,
				param: parameters,
				commandType: commandType
			);
		}

		/// <inheritdoc/>
		public override Task<TModel> RetrieveByIdAsync<TModel>(long id)
		{
			// Log retrieve
			var query = Queries.GetRetrieveQuery(Table, GetColumns<TModel>(), nameof(IEntity.Id), id);
			LogOperation(Operation.RetrieveById, "{Query} {Id}", query, id);

			// Perform retrieve and map to model
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: new { id },
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public override Task<TModel> UpdateAsync<TModel>(TEntity entity)
		{
			// Log update
			var query = Queries.GetUpdateQuery(Table, GetColumns<TModel>(), nameof(IEntity.Id), entity.Id);
			LogOperation(Operation.Update, "{Query} {@Entity}", query, entity);

			// Perform update and return updated entity
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: entity,
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public override Task<bool> DeleteAsync(long id)
		{
			// Log delete
			var query = Queries.GetDeleteQuery(Table, nameof(IEntity.Id), id);
			LogOperation(Operation.Delete, "{Query} {Id}", query, id);

			// Perform retrieve and map to model
			return Connection.ExecuteScalarAsync<bool>(
				sql: query,
				param: new { id },
				commandType: CommandType.Text
			);
		}

		#endregion
	}
}

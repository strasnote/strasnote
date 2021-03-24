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

		internal IDbQueries QueriesTest =>
			Queries;

		/// <summary>
		/// Table name
		/// </summary>
		protected string Table { get; private init; }

		internal string TableTest =>
			Table;

		/// <summary>
		/// Inject connection details
		/// </summary>
		/// <param name="client">IDbClient object</param>
		/// <param name="log">ILog (should be created with the context as the class implementing this abstract)</param>
		/// <param name="table">The table name for this entity</param>
		protected DbContextWithQueries(IDbClientWithQueries client, ILog log, string table) : base(client, log) =>
			(Queries, Table) = (client.Queries, table);

		#region Property Matching

		/// <summary>
		/// Cache matching model properties so expensive reflection isn't done more than once
		/// </summary>
		static private readonly ConcurrentDictionary<Type, List<string>> modelPropertyCache = new();

		internal int CacheTest;

		/// <summary>
		/// Get properties that match between the current entity and <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal List<string> GetProperties<TModel>() =>
			modelPropertyCache.GetOrAdd(typeof(TModel), _ =>
			{
				// Increase CacheTest
				CacheTest++;

				// Join on property name so only shared properties are returned
				var shared = from e in typeof(TEntity).GetProperties()
							 join m in typeof(TModel).GetProperties() on e.Name equals m.Name
							 where e.PropertyType == m.PropertyType
							 && e.GetCustomAttribute<IgnoreAttribute>() == null
							 && m.GetCustomAttribute<IgnoreAttribute>() == null
							 orderby e.Name
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
				string? name = property.Body switch
				{
					MemberExpression member =>
						member.Member.Name,

					UnaryExpression unary =>
						((MemberExpression)unary.Operand).Member.Name,

					_ =>
						null
				};

				if (name is not null)
				{
					where.Add((name, op, value));
				}
			}

			// Log retrieve
			var (query, param) = Queries.GetRetrieveQuery(Table, GetProperties<TModel>(), where);
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
		public override Task<TId> CreateAsync<TId>(TEntity entity)
		{
			// Log create
			var query = Queries.GetCreateQuery(Table, GetProperties<TEntity>());
			LogOperation(Operation.Create, "{Query} {@Entity}", query, entity);

			// Perform create and return created entity
			return Connection.ExecuteScalarAsync<TId>(
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
			var query = Queries.GetRetrieveQuery(Table, GetProperties<TModel>(), nameof(IEntity.Id), id);
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
			var query = Queries.GetUpdateQuery(Table, GetProperties<TEntity>(), nameof(IEntity.Id), entity.Id);
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

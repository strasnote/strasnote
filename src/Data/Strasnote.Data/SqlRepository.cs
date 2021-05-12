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
using Strasnote.Data.Exceptions;
using Strasnote.Logging;

namespace Strasnote.Data
{
	/// <summary>
	/// Holds model property cache for all types of entity
	/// </summary>
	public abstract class SqlRepository
	{
		/// <summary>
		/// Cache matching model properties so expensive reflection isn't done more than once
		/// </summary>
		protected private ConcurrentDictionary<Type, List<string>> ModelPropertyCache { get; private set; } = new();

		internal void SetCache(ConcurrentDictionary<Type, List<string>>? cache) =>
			ModelPropertyCache = cache ?? new();
	}

	/// <summary>
	/// SQL Database Repository base class
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public abstract class SqlRepository<TEntity> : SqlRepository, ISqlRepository<TEntity>, IDisposable
		where TEntity : IEntity
	{
		/// <summary>
		/// Database connection
		/// </summary>
		protected IDbConnection Connection { get; private init; }

		/// <summary>
		/// Logger
		/// </summary>
		protected ILog Log { get; private init; }

		internal ILog LogTest =>
			Log;

		/// <summary>
		/// Database queries
		/// </summary>
		protected ISqlQueries Queries { get; private init; }

		internal ISqlQueries QueriesTest =>
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
		protected SqlRepository(ISqlClient client, ILog log, string table) =>
			(Connection, Log, Queries, Table) = (client.Connect(), log, client.Queries, table);

		#region Logging

		/// <summary>
		/// Override with your own Log method if you want the messages to be logged somewhere other
		/// than the standard trace
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Log message arguments</param>
		protected virtual void LogTrace(string message, params object[] args) =>
			Log.Trace(message, args);

		internal void LogTraceTest(string message, params object[] args) =>
			LogTrace(message, args);

		/// <summary>
		/// Return the name of <paramref name="method"/> with 'Async' suffix removed
		/// </summary>
		/// <param name="method">Method name</param>
		private string GetMethod(string method) =>
			method.Replace("Async", string.Empty);

		/// <summary>
		/// Return the name of <typeparamref name="TEntity"/> with 'Entity' suffix removed
		/// </summary>
		private string GetEntity() =>
			typeof(TEntity).Name.Replace("Entity", string.Empty);

		/// <summary>
		/// Log an operation using <see cref="LogTrace(string, object[])"/>
		/// </summary>
		/// <param name="method">The name of the method (will have 'Async' suffix removed)</param>
		/// <param name="detail">Log message detail</param>
		/// <param name="args">Log message args (should correspond to <paramref name="detail"/>)</param>
		protected void LogOperation(string method, string detail, params object[] args) =>
			LogTrace($"{GetMethod(method)} {GetEntity()} {detail}", args);

		/// <inheritdoc cref="LogOperation(string, string, object[])"/>
		protected void LogOperation(Operation operation, string detail, params object[] args) =>
			LogOperation(operation.ToString(), detail, args);

		#endregion

		#region Property Matching

		/// <summary>
		/// Get properties that match between the current entity and <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal List<string> GetProperties<TModel>() =>
			ModelPropertyCache.GetOrAdd(typeof(TModel), _ =>
			{
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

				// Otherwise empty list - will therefore select all
				return new();
			});

		#endregion

		#region Custom Queries

		/// <inheritdoc/>
		public virtual Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param) =>
			QueryAsync<TModel>(query, param, CommandType.Text);

		/// <inheritdoc cref="QueryAsync{TModel}(string, object)"/>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command Type</param>
		public virtual Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param, CommandType type)
		{
			// Log query
			LogOperation(Operation.Query, "{Type}: {Query} - {@Parameters}", type, query, param);

			// Perform retrieve and map to TModel
			return Connection.QueryAsync<TModel>(
				sql: query,
				param: param,
				commandType: type
			);
		}

		/// <inheritdoc/>
		public virtual Task<TModel> QuerySingleAsync<TModel>(string query, object param) =>
			QuerySingleAsync<TModel>(query, param, CommandType.Text);

		/// <inheritdoc cref="QuerySingleAsync{TModel}(string, object)"/>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command Type</param>
		public virtual Task<TModel> QuerySingleAsync<TModel>(string query, object param, CommandType type)
		{
			// Log query single
			LogOperation(Operation.QuerySingle, "{Type}: {Query} - {@Parameters}", type, query, param);

			// Perform retrieve and map to TModel
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: param,
				commandType: type
			);
		}

		/// <summary>
		/// Convert predicates to list of where columns, and then get the retrieve query
		/// </summary>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="predicates">Search predicates</param>
		private (string query, Dictionary<string, object> param) GetRetrieveQuery<TModel>(
			(Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
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

			// Get query
			return Queries.GetRetrieveQuery(Table, GetProperties<TModel>(), where);
		}

		/// <inheritdoc/>
		public virtual Task<IEnumerable<TModel>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		)
		{
			// Log retrieve
			var (query, param) = GetRetrieveQuery<TModel>(predicates);
			LogOperation(Operation.Retrieve, "{Query} - {@Parameters}", query, param);

			// Perform retrieve and map to TModel
			return Connection.QueryAsync<TModel>(
				sql: query,
				param: param,
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public virtual Task<TModel> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		)
		{
			// Log retrieve
			var (query, param) = GetRetrieveQuery<TModel>(predicates);
			LogOperation(Operation.RetrieveSingle, "{Query} - {@Parameters}", query, param);

			// Perform retrieve and map to TModel
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: param,
				commandType: CommandType.Text
			);
		}

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public virtual Task<long> CreateAsync(TEntity entity)
		{
			// Log create
			var query = Queries.GetCreateQuery(Table, GetProperties<TEntity>());
			LogOperation(Operation.Create, "{Query} {@Entity}", query, entity);

			// Perform create and return created entity ID
			return Connection.ExecuteScalarAsync<long>(
				sql: query,
				param: entity,
				commandType: CommandType.Text
			);
		}

		/// <inheritdoc/>
		public virtual Task<TModel> RetrieveAsync<TModel>(long id)
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
		public virtual async Task<TModel> UpdateAsync<TModel>(long id, TModel model)
		{
			// Log update
			var query = Queries.GetUpdateQuery(Table, GetProperties<TModel>(), nameof(IEntity.Id), id);
			LogOperation(Operation.Update, "{Query} {@Model}", query, model);

			// Perform update
			var updated = await Connection.ExecuteAsync(
				sql: query,
				param: model,
				commandType: CommandType.Text
			).ConfigureAwait(false);

			// If the update was successful, retrieve updated model
			if (updated > 0)
			{
				return await RetrieveAsync<TModel>(id).ConfigureAwait(false);
			}
			// Otherwise, log error and throw exception
			else
			{
				Log.Error("Unable to update {EntityType} with ID {Id} using Model {Model}.", typeof(TEntity), id, model);
				throw new RepositoryUpdateException<TEntity>(id);
			}
		}

		/// <inheritdoc/>
		public virtual Task<int> DeleteAsync(long id)
		{
			// Log delete
			var query = Queries.GetDeleteQuery(Table, nameof(IEntity.Id), id);
			LogOperation(Operation.Delete, "{Query} {Id}", query, id);

			// Perform delete and return the number of affected rows
			return Connection.ExecuteAsync(
				sql: query,
				param: new { id },
				commandType: CommandType.Text
			);
		}

		#endregion

		#region Dispose

		/// <summary>
		/// Set to true if the object has been disposed
		/// </summary>
		private bool disposed;

		/// <summary>
		/// Suppress garbage collection and call <see cref="Dispose(bool)"/>
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose managed resources
		/// </summary>
		/// <param name="disposing">True if disposing</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				Connection.Dispose();
			}

			disposed = true;
		}

		#endregion
	}
}

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
	public abstract class SqlRepository<TEntity> : SqlRepository, ISqlRepository<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Database client
		/// </summary>
		protected ISqlClient Client { get; private init; }

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
			(Client, Log, Queries, Table) = (client, log, client.Queries, table);

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
		public virtual async Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param, CommandType type)
		{
			// Log query
			LogOperation(Operation.Query, "{Type}: {Query} - {@Parameters}", type, query, param);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform retrieve and map to TModel
			return await connection.QueryAsync<TModel>(
				sql: query,
				param: param,
				commandType: type
			).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<TModel> QuerySingleAsync<TModel>(string query, object param) =>
			QuerySingleAsync<TModel>(query, param, CommandType.Text);

		/// <inheritdoc cref="QuerySingleAsync{TModel}(string, object)"/>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command Type</param>
		public virtual async Task<TModel> QuerySingleAsync<TModel>(string query, object param, CommandType type)
		{
			// Log query single
			LogOperation(Operation.QuerySingle, "{Type}: {Query} - {@Parameters}", type, query, param);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform retrieve and map to TModel
			return await connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: param,
				commandType: type
			).ConfigureAwait(false);
		}

		/// <summary>
		/// Convert predicates to list of where columns, and then get the retrieve query
		/// </summary>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="predicates">Search predicates</param>
		/// <param name="userId">Current User ID</param>
		private (string query, Dictionary<string, object> param) GetRetrieveQuery<TModel>(
			(Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates,
			long? userId
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
			return Queries.GetRetrieveQuery(Table, GetProperties<TModel>(), where, userId);
		}

		/// <inheritdoc/>
		public virtual async Task<IEnumerable<TModel>> QueryAsync<TModel>(
			long? userId,
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		)
		{
			// Log retrieve
			var (query, param) = GetRetrieveQuery<TModel>(predicates, userId);
			LogOperation(Operation.Retrieve, "{Query} - {@Parameters}", query, param);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform retrieve and map to TModel
			return await connection.QueryAsync<TModel>(
				sql: query,
				param: param,
				commandType: CommandType.Text
			).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual async Task<TModel> QuerySingleAsync<TModel>(
			long? userId,
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		)
		{
			// Log retrieve
			var (query, param) = GetRetrieveQuery<TModel>(predicates, userId);
			LogOperation(Operation.RetrieveSingle, "{Query} - {@Parameters}", query, param);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform retrieve and map to TModel
			return await connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				param: param,
				commandType: CommandType.Text
			).ConfigureAwait(false);
		}

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public virtual async Task<long> CreateAsync(TEntity entity)
		{
			// Log create
			var query = Queries.GetCreateQuery(Table, GetProperties<TEntity>());
			LogOperation(Operation.Create, "{Query} {@Entity}", query, entity);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform create and return created entity ID
			return await connection.ExecuteScalarAsync<long>(
				sql: query,
				param: entity,
				commandType: CommandType.Text
			).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual async Task<TModel> RetrieveAsync<TModel>(long entityId, long? userId)
		{
			// Log retrieve
			var query = Queries.GetRetrieveQuery(Table, GetProperties<TModel>(), entityId, userId);
			LogOperation(Operation.RetrieveById, "{Query} | Entity: {EntityId} | User: {UserId}", query, entityId, userId ?? 0);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform retrieve and map to model
			return await connection.QuerySingleOrDefaultAsync<TModel>(
				sql: query,
				commandType: CommandType.Text
			).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual async Task<TModel> UpdateAsync<TModel>(long entityId, TModel model, long? userId)
		{
			// Log update
			var query = Queries.GetUpdateQuery(Table, GetProperties<TModel>(), entityId, userId);
			LogOperation(Operation.Update, "{Query} | Entity: {EntityId} | User: {UserId} | Model: {@Model}", query, entityId, userId ?? 0, model ?? new object());

			// Connect to the database
			using var connection = Client.Connect();

			// Perform update
			var updated = await connection.ExecuteAsync(
				sql: query,
				param: model,
				commandType: CommandType.Text
			).ConfigureAwait(false);

			// If the update was successful, retrieve updated model
			if (updated > 0)
			{
				return await RetrieveAsync<TModel>(entityId, userId).ConfigureAwait(false);
			}
			// Otherwise, log error and throw exception
			else
			{
				Log.Error("Unable to update {Entity} with ID {Id} (User {UserId}) using Model {Model}.", typeof(TEntity), entityId, userId ?? 0, model ?? new object());
				throw new RepositoryUpdateException<TEntity>(entityId);
			}
		}

		/// <inheritdoc/>
		public virtual async Task<int> DeleteAsync(long id, long? userId)
		{
			// Log delete
			var query = Queries.GetDeleteQuery(Table, id, userId);
			LogOperation(Operation.Delete, "{Query} | Entity: {Id} | User: {UserId}", query, id, userId ?? 0);

			// Connect to the database
			using var connection = Client.Connect();

			// Perform delete and return the number of affected rows
			return await connection.ExecuteAsync(
				sql: query,
				param: new { id },
				commandType: CommandType.Text
			).ConfigureAwait(false);
		}

		#endregion
	}
}

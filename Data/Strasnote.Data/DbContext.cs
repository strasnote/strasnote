// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Data
{
	/// <inheritdoc cref="IDbContext{TEntity}"/>
	public abstract class DbContext<TEntity> : IDbContext<TEntity>
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

		/// <summary>
		/// Inject connection details
		/// </summary>
		/// <param name="client">IDbClient object</param>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="log">ILog (should be created with the context as the class implementing this abstract)</param>
		protected DbContext(IDbClient client, string connectionString, ILog log) =>
			(Connection, Log) = (client.Connect(connectionString), log);

		/// <summary>
		/// Get the name of the stored procedure for the specified operation
		/// </summary>
		/// <param name="operation">CRUD operation</param>
		private string GetStoredProcedure(Operation operation) =>
			string.Format("{0}_{1}", typeof(TEntity).Name.Replace("Entity", string.Empty), operation);

		/// <summary>
		/// Log an operation using Log.Trace
		/// </summary>
		/// <param name="operation">CRUD operation</param>
		/// <param name="detail">Log message detail</param>
		/// <param name="args">Log message args (should correspond to <paramref name="detail"/>)</param>
		protected virtual void LogOperation(string operation, string detail, params object[] args) =>
			Log.Trace($"{operation} {typeof(TEntity)} {detail}", args);

		/// <inheritdoc cref="LogOperation(string, string, object[])"/>
		protected void LogOperation(Operation operation, string detail, params object[] args) =>
			LogOperation(operation.ToString(), detail, args);

		/// <inheritdoc/>
		public virtual Task<TModel> CreateAsync<TModel>(TEntity entity)
		{
			// Log create
			LogOperation(Operation.Create, "{Entity}", entity);

			// Perform create and return created entity
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: GetStoredProcedure(Operation.Create),
				param: entity,
				commandType: CommandType.StoredProcedure
			);
		}

		/// <inheritdoc/>
		public virtual Task<IEnumerable<TModel>> RetrieveAsync<TModel>(string query, object parameters, CommandType commandType)
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
		public virtual Task<TModel> RetrieveByIdAsync<TModel>(long id)
		{
			// Log retrieve
			LogOperation(Operation.RetrieveById, "{Id}", id);

			// Perform retrieve and map to model
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: GetStoredProcedure(Operation.RetrieveById),
				param: new { id },
				commandType: CommandType.StoredProcedure
			);
		}

		/// <inheritdoc/>
		public virtual Task<TModel> UpdateAsync<TModel>(TEntity entity)
		{
			// Log update
			LogOperation(Operation.Update, "{Entity}", entity);

			// Perform update and return updated entity
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: GetStoredProcedure(Operation.Update),
				param: entity,
				commandType: CommandType.StoredProcedure
			);
		}

		/// <inheritdoc/>
		public virtual Task<bool> DeleteAsync(long id)
		{
			// Log delete
			LogOperation(Operation.Delete, "{Id}", id);

			// Perform retrieve and map to model
			return Connection.ExecuteScalarAsync<bool>(
				sql: GetStoredProcedure(Operation.Delete),
				param: new { id },
				commandType: CommandType.StoredProcedure
			);
		}
	}
}

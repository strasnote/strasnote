// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Data
{
	/// <inheritdoc cref="IDbContext{TEntity}"/>
	public abstract class DbContext<TEntity> : IDbContext<TEntity>, IDisposable
		where TEntity : IEntity
	{
		/// <summary>
		/// Database connection
		/// </summary>
		protected IDbConnection Connection { get; private init; }

		/// <summary>
		/// Logger
		/// </summary>
		internal ILog Log { get; private init; }

		/// <summary>
		/// Inject connection details
		/// </summary>
		/// <param name="client">IDbClient object</param>
		/// <param name="log">ILog (should be created with the context as the class implementing this abstract)</param>
		protected DbContext(IDbClient client, ILog log) =>
			(Connection, Log) = (client.Connect(), log);

		/// <summary>
		/// Return the name of <typeparamref name="TEntity"/> with 'Entity' suffix removed
		/// </summary>
		private string GetEntity() =>
			typeof(TEntity).Name.Replace("Entity", string.Empty);

		/// <summary>
		/// Return the name of <paramref name="method"/> with 'Async' suffix removed
		/// </summary>
		/// <param name="method">Method name</param>
		private string GetMethod(string method) =>
			method.Replace("Async", string.Empty);

		#region Stored Procedures

		/// <summary>
		/// Get the name of the stored procedure for the specified operation
		/// </summary>
		/// <param name="method">The name of the method (will have 'Async' suffix removed)</param>
		protected string GetStoredProcedureName(string method) =>
			string.Format("{0}_{1}", GetEntity(), GetMethod(method));

		internal string GetStoredProcedureNameTest(string method) =>
			GetStoredProcedureName(method);

		/// <inheritdoc cref="GetStoredProcedureName(string)"/>
		protected string GetStoredProcedureName(Operation operation) =>
			GetStoredProcedureName(operation.ToString());

		internal string GetStoredProcedureNameTest(Operation operation) =>
			GetStoredProcedureName(operation);

		#endregion

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

		#region Standard CRUD Operations

		/// <inheritdoc/>
		public virtual Task<TModel> CreateAsync<TModel>(TEntity entity)
		{
			// Log create
			LogOperation(Operation.Create, "{Entity}", entity);

			// Perform create and return created entity
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: GetStoredProcedureName(Operation.Create),
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
				sql: GetStoredProcedureName(Operation.RetrieveById),
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
				sql: GetStoredProcedureName(Operation.Update),
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
				sql: GetStoredProcedureName(Operation.Delete),
				param: new { id },
				commandType: CommandType.StoredProcedure
			);
		}

		#endregion

		#region Dispose

		/// <summary>
		/// Set to true if the object has been disposed
		/// </summary>
		private bool disposed = false;

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

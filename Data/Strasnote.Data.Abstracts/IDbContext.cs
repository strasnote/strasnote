// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Base database context
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IDbContext<TEntity>
		where TEntity : IEntity
	{
		#region Custom Queries

		/// <summary>
		/// Retrieve entities using the specified query - can be a typed query or a stored procedure
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param, CommandType type);

		/// <summary>
		/// Retrieve a single entity using the specified query - can be a typed query or a stored procedure
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<TModel> QuerySingleAsync<TModel>(string query, object param, CommandType type);

		/// <summary>
		/// Retrieve entities using the specified predicates (uses AND)
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="predicates">List of predicates (uses AND)</param>
		Task<IEnumerable<TModel>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		);

		/// <summary>
		/// Retrieve a single entity using the specified predicates (uses AND)
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="predicates">List of predicates (uses AND)</param>
		Task<TModel> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		);

		#endregion

		#region CRUD Queries

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <param name="entity">Entity to create</param>
		Task<TModel> CreateAsync<TModel>(TEntity entity);

		/// <summary>
		/// Retrieve entities using a parameterised query
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">The query to execute</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="commandType">Defines what <paramref name="query"/> is - usually <see cref="CommandType.Text"/> or <see cref="CommandType.StoredProcedure"/></param>
		Task<IEnumerable<TModel>> RetrieveAsync<TModel>(string query, object parameters, CommandType commandType);

		/// <summary>
		/// Retrieve a single entity by ID
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="id">ID of entity to retrieve</param>
		Task<TModel> RetrieveByIdAsync<TModel>(long id);

		/// <summary>
		/// Update an entity
		/// </summary>
		/// <param name="entity">Entity to update</param>
		Task<TModel> UpdateAsync<TModel>(TEntity entity);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="id">ID of entity to delete</param>
		Task<bool> DeleteAsync(long id);

		#endregion
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Base database repository for an entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IRepository<TEntity>
		where TEntity : IEntity
	{
		#region Custom Queries

		/// <summary>
		/// Retrieve entities using the specified query
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param);

		/// <summary>
		/// Retrieve a single entity using the specified query
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		Task<TModel> QuerySingleAsync<TModel>(string query, object param);

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
		/// Create an entity and return new ID
		/// </summary>
		/// <param name="entity">Entity to create</param>
		Task<long> CreateAsync(TEntity entity);

		/// <summary>
		/// Retrieve a single entity by ID
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="id">ID of entity to retrieve</param>
		Task<TModel> RetrieveAsync<TModel>(long id);

		/// <summary>
		/// Update an entity and return updated model
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="entity">Entity to update</param>
		Task<TModel> UpdateAsync<TModel>(TEntity entity);

		/// <summary>
		/// Delete an entity and return the number of affected rows
		/// </summary>
		/// <param name="id">ID of entity to delete</param>
		Task<int> DeleteAsync(long id);

		#endregion
	}
}

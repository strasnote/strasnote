// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Data;
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
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Base database repository for an entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface ISqlRepository<TEntity> : IRepository<TEntity>
		where TEntity : IEntity
	{
		#region Custom Queries

		/// <summary>
		/// Retrieve entities using the specified query, either typed or a Stored Procedure
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command Type</param>
		Task<IEnumerable<TModel>> QueryAsync<TModel>(string query, object param, CommandType type);

		/// <summary>
		/// Retrieve a single entity using the specified query, either typed or a Stored Procedure
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="query">Query - text or stored procedure</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command Type</param>
		Task<TModel?> QuerySingleAsync<TModel>(string query, object param, CommandType type);

		#endregion
	}
}

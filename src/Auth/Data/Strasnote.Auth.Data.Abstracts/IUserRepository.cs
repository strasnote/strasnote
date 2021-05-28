// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the user / authentication / identity database
	/// </summary>
	public interface IUserRepository : ISqlRepository<UserEntity>
	{
		/// <summary>
		/// Retrieve a User by email
		/// </summary>
		/// <param name="email">Email address</param>
		Task<TModel> RetrieveByEmailAsync<TModel>(string email);

		/// <summary>
		/// Retrieve a User by username
		/// </summary>
		/// <param name="name">Username</param>
		Task<TModel> RetrieveByUsernameAsync<TModel>(string name);

		#region CRUD Queries

		/// <summary>
		/// Retrieve a single entity by ID
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="entityId">ID of entity to retrieve</param>
		Task<TModel> RetrieveAsync<TModel>(long entityId);

		/// <summary>
		/// Update an entity and return updated model
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="entityId">ID of entity to update</param>
		/// <param name="model">Model containing properties to update</param>
		Task<TModel> UpdateAsync<TModel>(long entityId, TModel model);

		/// <summary>
		/// Delete an entity and return the number of affected rows
		/// </summary>
		/// <param name="entityId">ID of entity to delete</param>
		Task<int> DeleteAsync(long entityId);

		#endregion
	}
}

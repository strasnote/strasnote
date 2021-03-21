// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the user role database.
	/// </summary>
	public interface IRoleContext : IDbContext<RoleEntity>
	{
		/// <summary>
		/// Retrieve a role by <paramref name="roleName"/>
		/// </summary>
		/// <param name="roleName">Role name</param>
		Task<TModel> RetrieveByNameAsync<TModel>(string roleName);

		/// <summary>
		/// Retrieve the roles for a user, by <paramref name="userId"/>
		/// </summary>
		/// <param name="userId">User ID</param>
		Task<IEnumerable<TModel>> RetrieveForUserAsync<TModel>(long userId);
	}
}

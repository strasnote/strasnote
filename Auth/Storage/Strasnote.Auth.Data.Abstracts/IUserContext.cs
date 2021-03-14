// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the user / authentication / identity database
	/// </summary>
	public interface IUserContext
	{
		/// <summary>
		/// Create a user
		/// </summary>
		/// <param name="user">User entity</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieve a User by ID
		/// </summary>
		/// <param name="id">User ID</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<UserEntity> RetrieveAsync(int id, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieve a User by username
		/// </summary>
		/// <param name="name">Username</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<UserEntity> RetrieveAsync(string name, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieve a User by email
		/// </summary>
		/// <param name="email">Email</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<UserEntity> RetrieveByEmail(string email, CancellationToken cancellationToken);

		/// <summary>
		/// Update a user
		/// </summary>
		/// <param name="user">User entity</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken);

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="user">User entity</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken);
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the refresh token database.
	/// </summary>
	public interface IRefreshTokenContext : IRepository<RefreshTokenEntity>
	{
		/// <summary>
		/// Create an entity but do not return anything afterwards
		/// </summary>
		/// <param name="entity">Refresh Token to create</param>
		Task CreateAsync(RefreshTokenEntity entity);

		/// <summary>
		/// Retrieve a refresh token by <paramref name="userId"/> and <paramref name="refreshToken"/>
		/// </summary>
		/// <param name="userId">The user's ID</param>
		/// <param name="refreshToken">The user's current refresh token</param>
		Task<RefreshTokenEntity> RetrieveForUserAsync(long userId, string refreshToken);

		/// <summary>
		/// Delete a refresh token by <paramref name="userId"/>
		/// </summary>
		/// <param name="userId">The user's ID</param>
		Task<bool> DeleteByUserIdAsync(long userId);
	}
}

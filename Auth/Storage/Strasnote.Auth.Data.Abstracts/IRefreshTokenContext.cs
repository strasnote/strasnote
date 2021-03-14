// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the refresh token database.
	/// </summary>
	public interface IRefreshTokenContext
	{
		/// <summary>
		/// Create a <paramref name="refreshToken"/>.
		/// </summary>
		/// <param name="refreshToken">Refresh token entity.</param>
		Task<RefreshTokenEntity> CreateAsync(RefreshTokenEntity refreshToken);

		/// <summary>
		/// Retrieve a refresh token by <paramref name="userId"/> and <paramref name="refreshToken"/>.
		/// </summary>
		/// <param name="userId">The user's ID.</param>
		/// <param name="refreshToken">The user's current refresh token.</param>
		Task<RefreshTokenEntity> Retrieve(long userId, string refreshToken);

		/// <summary>
		/// Delete a refresh token by <paramref name="userId"/>.
		/// </summary>
		/// <param name="userId">The user's ID</param>
		Task<bool> DeleteByUserIdAsync(long userId);
	}
}

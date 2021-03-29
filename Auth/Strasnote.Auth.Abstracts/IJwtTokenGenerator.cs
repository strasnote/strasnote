// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Abstracts
{
	/// <summary>
	/// Used to generate a JWT token for a user
	/// </summary>
	public interface IJwtTokenGenerator
	{
		/// <summary>
		/// Generate a JWT access token for the <paramref name="user"/>
		/// </summary>
		/// <param name="user">The UserEntity</param>
		/// <returns>A new access token</returns>
		Task<string> GenerateAccessTokenAsync(UserEntity user);

		/// <summary>
		/// Generate a refresh token for the <paramref name="user"/>
		/// </summary>
		/// <param name="user">The UserEntity</param>
		/// <returns>A new RefreshTokenEntity with the user's refresh token</returns>
		RefreshTokenEntity GenerateRefreshToken(UserEntity user);
	}
}

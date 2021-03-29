// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Auth.Models;

namespace Strasnote.Auth.Abstracts
{
	/// <summary>
	/// Used to issue a JWT token to the user
	/// </summary>
	public interface IJwtTokenIssuer
	{
		/// <summary>
		/// Get token content
		/// </summary>
		Task<TokenResponse> GetTokenAsync(string email, string password);

		/// <summary>
		/// Get refresh token
		/// </summary>
		Task<TokenResponse> GetRefreshTokenAsync(string accessToken, string refreshToken);
	}
}

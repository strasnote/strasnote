// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Auth.Models;

namespace Strasnote.Auth.Abstracts
{
	/// <summary>
	/// JSON Web Token
	/// </summary>
	public interface IJwtToken
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

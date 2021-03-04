using System;
using System.Threading.Tasks;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Models;

namespace Strasnote.Auth
{
	/// <inheritdoc cref="IJwtToken"/>
	public class JwtToken : IJwtToken
	{
		/// <inheritdoc/>
		public async Task<TokenResponse> GetRefreshTokenAsync()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public async Task<TokenResponse> GetTokenAsync()
		{
			throw new NotImplementedException();
		}
	}
}

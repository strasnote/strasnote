using System;
using System.Threading.Tasks;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Models;

namespace Strasnote.Auth
{
	public class JwtToken : IJwtToken
	{
		public Task<TokenResponse> GetRefreshToken()
		{
			throw new NotImplementedException();
		}

		public Task<TokenResponse> GetToken()
		{
			throw new NotImplementedException();
		}
	}
}

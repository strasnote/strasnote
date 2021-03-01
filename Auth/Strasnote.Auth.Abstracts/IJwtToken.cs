using Strasnote.Auth.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strasnote.Auth.Abstracts
{
	public interface IJwtToken
	{
		Task<TokenResponse> GetToken();

		Task<TokenResponse> GetRefreshToken();
	}
}

using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

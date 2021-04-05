// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Strasnote.Auth.Config
{
	/// <summary>
	/// Create <see cref="TokenValidationParameters"/> from <see cref="AuthConfig"/>
	/// </summary>
	public class AuthConfigTokenValidationParameters : TokenValidationParameters
	{
		/// <summary>
		/// Inject <see cref="IConfiguration"/> to get configuration for creating parameters
		/// </summary>
		/// <param name="config">IConfiguration</param>
		public AuthConfigTokenValidationParameters(IConfiguration config)
		{
			if (config.GetSection("Auth").Get<AuthConfig>() is AuthConfig authConfig)
			{
				ValidIssuer = authConfig.Jwt.Issuer;
				ValidAudience = authConfig.Jwt.Audience;
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Jwt.Secret));
				RequireExpirationTime = true;
				ValidateIssuer = true;
				ValidateIssuerSigningKey = true;
				ValidateAudience = true;
				ValidateLifetime = true;
				ClockSkew = TimeSpan.Zero;
			}
		}
	}
}

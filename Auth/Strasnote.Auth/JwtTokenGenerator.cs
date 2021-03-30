// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;

namespace Strasnote.Auth
{
	/// <inheritdoc cref="IJwtTokenGenerator"/>
	internal sealed class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly AuthConfig authConfig;
		private readonly IUserManager userManager;

		public JwtTokenGenerator(
			IOptions<AuthConfig> authConfig,
			IUserManager userManager)
		{
			this.authConfig = authConfig.Value;
			this.userManager = userManager;
		}

		/// <inheritdoc/>
		public async Task<string> GenerateAccessTokenAsync(UserEntity user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secret = Encoding.ASCII.GetBytes(authConfig.Jwt.Secret);

			// ToDo: split this into at least two methods - GetSecurityTokenDescriptor() and WriteToken().

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(),
				Expires = DateTime.UtcNow.AddMinutes(authConfig.Jwt.TokenExpiryMinutes),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature),
				Issuer = authConfig.Jwt.Issuer,
				Audience = authConfig.Jwt.Audience
			};

			// Get the user's roles and add them as claims
			var roles = await userManager.GetRolesAsync(user);

			if(roles != null)
			{
				foreach (var role in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role));
				}
			}			

			// Add all the claims to the token descriptor
			tokenDescriptor.Subject.AddClaims(claims);

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		/// <inheritdoc/>
		public RefreshTokenEntity GenerateRefreshToken(UserEntity userEntity)
		{
			var token = Rnd.RndString.Get(50, numbers: true, special: true);
			var hashedToken = userManager.PasswordHasher.HashPassword(userEntity, token);

			var tokenExpiry = DateTimeOffset.Now.AddMinutes(authConfig.Jwt.RefreshTokenExpiryMinutes);

			return new(hashedToken, tokenExpiry, userEntity.Id);
		}
	}
}

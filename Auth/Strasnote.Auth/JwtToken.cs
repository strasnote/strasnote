// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Models;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;

namespace Strasnote.Auth
{
	/// <inheritdoc cref="IJwtToken"/>
	public class JwtToken : IJwtToken
	{
		private readonly UserManager<UserEntity> userManager;
		private readonly SignInManager<UserEntity> signInManager;
		private readonly AuthConfig authConfig;
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
		private readonly IRefreshTokenContext refreshTokenContext;

		public JwtToken(
			UserManager<UserEntity> userManager,
			SignInManager<UserEntity> signInManager,
			IOptions<AuthConfig> authConfig,
			JwtSecurityTokenHandler jwtSecurityTokenHandler,
			IRefreshTokenContext refreshTokenContext)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.authConfig = authConfig.Value;
			this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
			this.refreshTokenContext = refreshTokenContext;
		}

		/// <inheritdoc/>
		public async Task<TokenResponse> GetTokenAsync(string email, string password)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return new TokenResponse("User does not exist", false);
			}

			var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, true);

			if (signInResult.IsLockedOut)
			{
				return new TokenResponse("User locked out", false);
			}

			if (!signInResult.Succeeded)
			{
				return new TokenResponse("User login failed", false);
			}

			var refreshToken = GenerateRefreshTokenAsync(user);

			await refreshTokenContext.DeleteByUserIdAsync(user.Id);
			await refreshTokenContext.CreateAsync(refreshToken);

			var accessToken = await GenerateTokenAsync(user);

			return new(accessToken, refreshToken.RefreshTokenValue);
		}

		/// <inheritdoc/>
		public async Task<TokenResponse> GetRefreshTokenAsync(string accessToken, string refreshToken)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Jwt.Secret));

			var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = securityKey,
				ValidateLifetime = false, // ToDo: review this
				ValidAudience = authConfig.Jwt.Audience,
				ValidIssuer = authConfig.Jwt.Issuer,
				ClockSkew = TimeSpan.Zero
			}, out _);

			if (claimsPrincipal == null)
			{
				return new("Failed to validate existing access token", false);
			}

			// Get the user's ID from their claims
			var userId = claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
			var user = await userManager.FindByIdAsync(userId);

			var existingRefreshToken = await refreshTokenContext.Retrieve(user.Id, refreshToken);

			if (existingRefreshToken == null)
			{
				return new("Refresh token invalid", false);
			}

			if (existingRefreshToken.RefreshTokenExpires <= DateTimeOffset.Now)
			{
				return new("Refresh token has expired", false);
			}

			await refreshTokenContext.DeleteByUserIdAsync(user.Id);

			var newRefreshToken = GenerateRefreshTokenAsync(user);

			await refreshTokenContext.CreateAsync(newRefreshToken);

			var newAccessToken = await GenerateTokenAsync(user);

			return new(newAccessToken, newRefreshToken.RefreshTokenValue);
		}

		private async Task<string> GenerateTokenAsync(UserEntity user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secret = Encoding.ASCII.GetBytes(authConfig.Jwt.Secret);

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
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			// Add all the claims to the token descriptor
			tokenDescriptor.Subject.AddClaims(claims);

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return await Task.FromResult(tokenHandler.WriteToken(token));
		}

		private RefreshTokenEntity GenerateRefreshTokenAsync(UserEntity userEntity)
		{
			var token = Rnd.RndString.Get(50, numbers: true, special: true);
			var hashedToken = userManager.PasswordHasher.HashPassword(userEntity, token);

			var tokenExpiry = DateTimeOffset.Now.AddMinutes(authConfig.Jwt.RefreshTokenExpiryMinutes);

			return new(hashedToken, tokenExpiry, userEntity.Id);
		}
	}
}

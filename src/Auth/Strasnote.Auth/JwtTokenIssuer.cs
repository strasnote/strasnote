// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Exceptions;
using Strasnote.Auth.Models;

namespace Strasnote.Auth
{
	/// <inheritdoc cref="IJwtTokenIssuer"/>
	public class JwtTokenIssuer : IJwtTokenIssuer
	{
		private readonly IUserManager userManager;
		private readonly ISignInManager signInManager;
		private readonly AuthConfig authConfig;
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
		private readonly IRefreshTokenRepository refreshTokenRepository;
		private readonly IJwtTokenGenerator jwtTokenGenerator;

		public JwtTokenIssuer(
			IUserManager userManager,
			ISignInManager signInManager,
			IOptions<AuthConfig> authConfig,
			JwtSecurityTokenHandler jwtSecurityTokenHandler,
			IRefreshTokenRepository refreshTokenRepository,
			IJwtTokenGenerator jwtTokenGenerator)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.authConfig = authConfig.Value;
			this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
			this.refreshTokenRepository = refreshTokenRepository;
			this.jwtTokenGenerator = jwtTokenGenerator;
		}

		/// <inheritdoc/>
		public async Task<TokenResponse> GetTokenAsync(string email, string password)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					return new TokenResponse("Email/password not supplied", false);
				}

				var user = await userManager.FindByEmailAsync(email);
				var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, true);

				if (signInResult.IsLockedOut)
				{
					return new TokenResponse("User locked out", false);
				}

				if (!signInResult.Succeeded)
				{
					return new TokenResponse("User login failed", false);
				}

				var refreshToken = jwtTokenGenerator.GenerateRefreshToken(user);

				await refreshTokenRepository.DeleteByUserIdAsync(user.Id);
				await refreshTokenRepository.CreateAsync(refreshToken);

				var accessToken = await jwtTokenGenerator.GenerateAccessTokenAsync(user);

				return new(accessToken, refreshToken.RefreshTokenValue);
			}
			catch (UserNotFoundException)
			{
				return new TokenResponse("User does not exist", false);
			}
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
				ValidateLifetime = true,
				ValidAudience = authConfig.Jwt.Audience,
				ValidIssuer = authConfig.Jwt.Issuer,
				ClockSkew = TimeSpan.Zero
			}, out _);

			if (claimsPrincipal == null)
			{
				return new("Failed to validate existing access token", false);
			}

			// Get the user's ID from their claims
			var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
			{
				return new("Could not find user from access token", false);
			}

			var user = await userManager.FindByIdAsync(userId);

			var existingRefreshToken = await refreshTokenRepository.RetrieveForUserAsync(user.Id, refreshToken);

			if (existingRefreshToken == null)
			{
				return new("Refresh token invalid", false);
			}

			if (existingRefreshToken.RefreshTokenExpires <= DateTime.UtcNow)
			{
				return new("Refresh token has expired", false);
			}

			await refreshTokenRepository.DeleteByUserIdAsync(user.Id);

			var newRefreshToken = jwtTokenGenerator.GenerateRefreshToken(user);

			await refreshTokenRepository.CreateAsync(newRefreshToken);

			var newAccessToken = await jwtTokenGenerator.GenerateAccessTokenAsync(user);

			return new(newAccessToken, newRefreshToken.RefreshTokenValue);
		}
	}
}

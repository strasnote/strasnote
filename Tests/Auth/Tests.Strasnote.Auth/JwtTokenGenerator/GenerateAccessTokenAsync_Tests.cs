// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Strasnote.Auth;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth
{
	public sealed class GenerateAccessTokenAsync_Tests
	{
		private readonly IOptions<AuthConfig> authConfig = Options.Create(new AuthConfig
		{
			Jwt = new JwtConfig
			{
				Secret = Rnd.RndString.Get(20),
				TokenExpiryMinutes = 5,
				Issuer = Rnd.Str,
				Audience = Rnd.Str
			}
		});

		private readonly IUserManager userManager = Substitute.For<IUserManager>();

		private UserEntity userEntity = new()
		{
			UserName = Rnd.Str,
			Id = Rnd.Int
		};

		public GenerateAccessTokenAsync_Tests()
		{
			userManager.GetRolesAsync(Arg.Any<UserEntity>())
				.Returns(new List<string>() { Rnd.Str });
		}

		[Fact]
		public async Task Valid_Jwt_Token_Is_Returned_On_Successful_Call()
		{
			// Arrange
			var jwtTokenGenerator = new JwtTokenGenerator(
				authConfig,
				userManager);

			// Act
			var result = await jwtTokenGenerator.GenerateAccessTokenAsync(userEntity);

			// Assert
			var tokenHandler = new JwtSecurityTokenHandler();
			var claimsPrincipal = tokenHandler.ValidateToken(result, new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = false,
				ValidateLifetime = false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Value.Jwt.Secret))
			}, out _);

			Assert.NotNull(claimsPrincipal);
		}

		[Fact]
		public async Task Valid_Jwt_Token_Is_Returned_Even_If_User_Has_No_Roles()
		{
			// Arrange
			userManager.GetRolesAsync(Arg.Any<UserEntity>())
				.ReturnsNull();

			var jwtTokenGenerator = new JwtTokenGenerator(
				authConfig,
				userManager);

			// Act
			var result = await jwtTokenGenerator.GenerateAccessTokenAsync(userEntity);

			// Assert
			var tokenHandler = new JwtSecurityTokenHandler();
			var claimsPrincipal = tokenHandler.ValidateToken(result, new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = false,
				ValidateLifetime = false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Value.Jwt.Secret))
			}, out _);

			Assert.NotNull(claimsPrincipal);
		}
	}
}

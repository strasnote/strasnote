// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.Options;
using NSubstitute;
using Strasnote.Auth;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth
{
	public sealed class GenerateRefreshToken_Tests
	{
		private readonly IOptions<AuthConfig> authConfig = Options.Create(new AuthConfig
		{
			Jwt = new JwtConfig
			{
				RefreshTokenExpiryMinutes = 60
			}
		});
		private readonly IUserManager userManager = Substitute.For<IUserManager>();

		private UserEntity userEntity = new()
		{
			UserName = Rnd.Str,
			Id = Rnd.Int,
			SecurityStamp = Guid.NewGuid().ToString()
		};

		public GenerateRefreshToken_Tests()
		{
			userManager.PasswordHasher.HashPassword(Arg.Any<UserEntity>(), Arg.Any<string>())
				.Returns(Rnd.RndString.Get(20));
		}

		[Fact]
		public void Hashed_Token_Is_Returned()
		{
			// Arrange
			var jwtTokenGenerator = new JwtTokenGenerator(
				authConfig,
				userManager);

			// Act
			var result = jwtTokenGenerator.GenerateRefreshToken(userEntity);

			// Assert
			Assert.False(string.IsNullOrWhiteSpace(result.RefreshTokenValue));
		}

		[Fact]
		public void Token_Expiry_Returned_Matches_Auth_Config_RefreshTokenExpiryMinutes()
		{
			// Arrange
			var jwtTokenGenerator = new JwtTokenGenerator(
				authConfig,
				userManager);

			// Act
			var result = jwtTokenGenerator.GenerateRefreshToken(userEntity);

			// Assert
			Assert.True(result.RefreshTokenExpires > DateTimeOffset.Now.AddMinutes(authConfig.Value.Jwt.RefreshTokenExpiryMinutes).AddSeconds(-10));
		}

		[Fact]
		public void UserEntity_Id_Is_Returned()
		{
			// Arrange
			var jwtTokenGenerator = new JwtTokenGenerator(
				authConfig,
				userManager);

			// Act
			var result = jwtTokenGenerator.GenerateRefreshToken(userEntity);

			// Assert
			Assert.Equal(result.UserId, userEntity.Id);
		}
	}
}

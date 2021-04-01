// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Strasnote.Auth;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth
{
	public sealed class GetRefreshTokenAsync_Tests
	{
		private readonly IUserManager userManager = Substitute.For<IUserManager>();
		private readonly ISignInManager signInManager = Substitute.For<ISignInManager>();
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
		private readonly IRefreshTokenRepository refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();
		private readonly IJwtTokenGenerator jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();

		private readonly IOptions<AuthConfig> authConfig = Options.Create<AuthConfig>(new AuthConfig
		{
			Jwt = new JwtConfig
			{
				Audience = Rnd.Str,
				Issuer = Rnd.Str,
				Secret = Rnd.RndString.Get(20),
				RefreshTokenExpiryMinutes = 60,
				TokenExpiryMinutes = 5
			}
		});

		private UserEntity userEntity = new()
		{
			Id = 1,
			UserName = Rnd.Str
		};

		public GetRefreshTokenAsync_Tests()
		{
			jwtSecurityTokenHandler.ValidateToken(Arg.Any<string>(), Arg.Any<TokenValidationParameters>(), out _)
				.Returns(new TestPrincipal(new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString())));

			userManager.FindByIdAsync(Arg.Any<string>())
				.Returns(userEntity);

			refreshTokenRepository.RetrieveForUserAsync(Arg.Any<long>(), Arg.Any<string>())
				.Returns(new RefreshTokenEntity(Rnd.Str, DateTimeOffset.Now.AddDays(1), userEntity.Id));

			jwtTokenGenerator.GenerateRefreshToken(Arg.Any<UserEntity>())
				.Returns(new RefreshTokenEntity(Rnd.Str, DateTimeOffset.Now.AddDays(1), userEntity.Id));

			jwtTokenGenerator.GenerateAccessTokenAsync(Arg.Any<UserEntity>())
				.Returns(Rnd.Str);
		}

		[Fact]
		public async Task Valid_Access_Token_And_Refresh_Token_Returns_New_Access_Token_And_New_Refresh_Token()
		{
			// Arrange
			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));
			Assert.False(string.IsNullOrWhiteSpace(result.RefreshToken));
		}

		[Fact]
		public async Task Invalid_AccessToken_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			jwtSecurityTokenHandler.ValidateToken(Arg.Any<string>(), Arg.Any<TokenValidationParameters>(), out _)
				.ReturnsNull();

			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task UserId_Null_In_ClaimsPrincipal_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			jwtSecurityTokenHandler.ValidateToken(Arg.Any<string>(), Arg.Any<TokenValidationParameters>(), out _)
				.Returns(new TestPrincipal());

			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Existing_Refresh_Token_Not_Found_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			refreshTokenRepository.RetrieveForUserAsync(Arg.Any<long>(), Arg.Any<string>())
				.ReturnsNull();

			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Existing_Refresh_Token_Expired_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			refreshTokenRepository.RetrieveForUserAsync(Arg.Any<long>(), Arg.Any<string>())
				.Returns(new RefreshTokenEntity()
				{
					RefreshTokenExpires = DateTime.Now.AddDays(-1)
				});

			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task RefreshTokenRepository_DeleteByUserIdAsync_Is_Called()
		{
			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			await refreshTokenRepository.Received().DeleteByUserIdAsync(Arg.Any<long>());
		}

		[Fact]
		public async Task RefreshTokenRepository_CreateAsync_Is_Called()
		{
			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			await refreshTokenRepository.Received().CreateAsync(Arg.Any<RefreshTokenEntity>());
		}

		[Fact]
		public async Task JwtTokenGenerator_GenerateRefreshToken_Is_Called()
		{
			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			jwtTokenGenerator.Received().GenerateRefreshToken(Arg.Any<UserEntity>());
		}

		[Fact]
		public async Task JwtTokenGenerator_GenerateAccessTokenAsync_Is_Called()
		{
			var jwtTokenIssuer = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenRepository,
				jwtTokenGenerator);

			// Act
			var result = await jwtTokenIssuer.GetRefreshTokenAsync(Rnd.Str, Rnd.Str);

			// Assert
			await jwtTokenGenerator.Received().GenerateAccessTokenAsync(Arg.Any<UserEntity>());
		}
	}
}

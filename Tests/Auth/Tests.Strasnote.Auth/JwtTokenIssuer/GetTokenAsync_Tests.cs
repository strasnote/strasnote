// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
	public sealed class GetTokenAsync_Tests
	{
		private readonly IUserManager userManager = Substitute.For<IUserManager>();
		private readonly ISignInManager signInManager = Substitute.For<ISignInManager>();
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
		private readonly IRefreshTokenContext refreshTokenContext = Substitute.For<IRefreshTokenContext>();
		private readonly IJwtTokenGenerator jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();

		private readonly IOptions<AuthConfig> authConfig = Options.Create(new AuthConfig
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

		private SignInResult signInResult = SignInResult.Success;

		public GetTokenAsync_Tests()
		{
			userManager.FindByEmailAsync(Arg.Any<string>())
				.Returns(userEntity);

			signInManager.CheckPasswordSignInAsync(Arg.Any<UserEntity>(), Arg.Any<string>(), Arg.Any<bool>())
				.Returns(signInResult);

			jwtTokenGenerator.GenerateRefreshToken(Arg.Any<UserEntity>())
				.Returns(new RefreshTokenEntity("token", DateTimeOffset.Now.AddDays(1), userEntity.Id));

			jwtTokenGenerator.GenerateAccessTokenAsync(Arg.Any<UserEntity>())
				.Returns("accessToken");
		}

		[Fact]
		public async Task Valid_Email_And_Password_Returns_Access_Token_And_Refresh_Token()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			Assert.NotNull(result.AccessToken);
			Assert.NotNull(result.RefreshToken);
		}

		[Fact]
		public async Task Valid_Email_And_Password_With_Special_Characters_Returns_Access_Token_And_Refresh_Token()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "\"!£$%^&*(-=😁");

			// Assert
			Assert.NotNull(result.AccessToken);
			Assert.NotNull(result.RefreshToken);
		}

		[Fact]
		public async Task Invalid_Email_With_Password_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test", Rnd.Str);

			// Assert
			Assert.NotNull(result.AccessToken);
			Assert.NotNull(result.RefreshToken);
		}

		[Fact]
		public async Task Invalid_Email_With_Special_Characters_With_Password_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("\"!£$%^&*(-=😁", Rnd.Str);

			// Assert
			Assert.NotNull(result.AccessToken);
			Assert.NotNull(result.RefreshToken);
		}

		[Fact]
		public async Task Non_Existing_User_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			userManager.FindByEmailAsync(Arg.Any<string>())
				.ReturnsNull();

			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Locked_Out_User_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			signInResult = SignInResult.LockedOut;

			signInManager.CheckPasswordSignInAsync(Arg.Any<UserEntity>(), Arg.Any<string>(), Arg.Any<bool>())
				.Returns(signInResult);

			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task If_SignInResult_Succeeded_Is_False_Return_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			signInResult = SignInResult.Failed;

			signInManager.CheckPasswordSignInAsync(Arg.Any<UserEntity>(), Arg.Any<string>(), Arg.Any<bool>())
				.Returns(signInResult);

			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task RefreshTokenContext_DeleteByUserIdAsync_Is_Called()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			await refreshTokenContext.Received().DeleteByUserIdAsync(Arg.Any<long>());
		}

		[Fact]
		public async Task RefreshTokenContext_CreateAsync_Is_Called()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			await refreshTokenContext.Received().CreateAsync(Arg.Any<RefreshTokenEntity>());
		}

		[Fact]
		public async Task Null_Email_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync(null!, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Null_Password_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange

			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", null!);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Blank_Password_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync(string.Empty, Rnd.Str);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task Blank_Email_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", string.Empty);

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task JwtTokenGenerator_GenerateRefreshToken_Is_Called()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			jwtTokenGenerator.Received().GenerateRefreshToken(Arg.Any<UserEntity>());
		}

		[Fact]
		public async Task JwtTokenGenerator_GenerateAccessTokenAsync_Is_Called()
		{
			// Act
			var jwtTokenService = new JwtTokenIssuer(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext,
				jwtTokenGenerator);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", Rnd.Str);

			// Assert
			await jwtTokenGenerator.Received().GenerateAccessTokenAsync(Arg.Any<UserEntity>());
		}
	}
}

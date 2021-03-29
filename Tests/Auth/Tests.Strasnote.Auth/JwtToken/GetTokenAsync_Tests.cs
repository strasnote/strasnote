using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
	public sealed class GetTokenAsync_Tests
	{
		private readonly IUserManager userManager = Substitute.For<IUserManager>();
		private readonly ISignInManager signInManager = Substitute.For<ISignInManager>();
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = Substitute.For<JwtSecurityTokenHandler>();
		private readonly IRefreshTokenContext refreshTokenContext = Substitute.For<IRefreshTokenContext>();

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
		}

		[Fact]
		public async Task Valid_Email_And_Password_Returns_Access_Token_And_Refresh_Token()
		{
			// Arrange

			// Act
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

			// Assert
			Assert.NotNull(result.AccessToken);
			Assert.NotNull(result.RefreshToken);
		}

		[Fact]
		public async Task Returned_Access_Token_Is_Valid()
		{
			// Arrange

			// Act
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

			// Assert
			var tokenHandler = new JwtSecurityTokenHandler();
			var claimsPrincipal = tokenHandler.ValidateToken(result.AccessToken, new TokenValidationParameters
			{
				ValidateLifetime = false,
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidIssuer = authConfig.Value.Jwt.Issuer,
				ValidAudience = authConfig.Value.Jwt.Issuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Value.Jwt.Secret))
			}, out _);

			Assert.NotNull(claimsPrincipal);
		}

		[Fact]
		public async Task Non_Existing_User_Returns_TokenResponse_With_Success_False_And_Error_Message()
		{
			// Arrange
			userManager.FindByEmailAsync(Arg.Any<string>())
				.ReturnsNull();

			// Act
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

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
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

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
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

			// Assert
			Assert.False(result.Success);
			Assert.False(string.IsNullOrWhiteSpace(result.Message));
		}

		[Fact]
		public async Task RefreshTokenContext_DeleteByUserIdAsync_Is_Called_When_Issuing_Token()
		{
			// Arrange

			// Act
			var jwtTokenService = new JwtToken(
				userManager,
				signInManager,
				authConfig,
				jwtSecurityTokenHandler,
				refreshTokenContext);

			var result = await jwtTokenService.GetTokenAsync("test@email.com", "password");

			// Assert
			await refreshTokenContext.Received().DeleteByUserIdAsync(Arg.Any<long>());
		}
	}
}

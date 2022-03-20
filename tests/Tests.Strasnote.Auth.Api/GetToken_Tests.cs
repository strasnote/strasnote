// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Api.Controllers;
using Strasnote.Auth.Models;
using Strasnote.Logging;
using static Strasnote.Auth.Api.Controllers.TokenController;

namespace Tests.Strasnote.Auth.Api
{
	public sealed class GetToken_Tests
	{
		private readonly IJwtTokenIssuer jwtTokenIssuer = Substitute.For<IJwtTokenIssuer>();
		private readonly ILog<TokenController> log = Substitute.For<ILog<TokenController>>();

		[Fact]
		public async Task Ok_Response_Returned_On_Successful_Call()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(new TokenResponse(Rnd.Str, true));

			// Act
			var result = await tokenController.GetToken(tokenRequest);
			var okResult = result as OkObjectResult;

			// Assert
			Assert.NotNull(okResult);
			Assert.Equal((int)HttpStatusCode.OK, okResult?.StatusCode);
		}

		[Fact]
		public async Task TokenResponseViewModel_Returned_On_Successful_Call()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			var tokenResponse = new TokenResponse(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(tokenResponse);

			// Act
			var result = await tokenController.GetToken(tokenRequest);
			var okResult = (OkObjectResult)result;

			// Assert
			Assert.IsAssignableFrom<TokenResponseViewModel>(okResult.Value);
		}

		[Fact]
		public async Task TokenResponseViewModel_Contains_Access_Token_And_Refresh_Token_On_Successful_Call()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			var tokenResponse = new TokenResponse(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(tokenResponse);

			// Act
			var result = await tokenController.GetToken(tokenRequest);
			var okResult = (OkObjectResult)result;
			var tokenResponseViewModel = (TokenResponseViewModel)okResult.Value!;

			// Assert
			Assert.False(string.IsNullOrWhiteSpace(tokenResponseViewModel.AccessToken));
			Assert.False(string.IsNullOrWhiteSpace(tokenResponseViewModel.RefreshToken));
			Assert.True(tokenResponseViewModel.Success);
		}

		[Fact]
		public async Task Token_Success_False_Returns_Unauthorized_Response()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(new TokenResponse(Rnd.Str, false));

			// Act
			var result = await tokenController.GetToken(tokenRequest);
			var unauthorizedResult = result as UnauthorizedObjectResult;

			// Assert
			Assert.NotNull(unauthorizedResult);
			Assert.Equal((int)HttpStatusCode.Unauthorized, unauthorizedResult?.StatusCode);
		}

		[Fact]
		public async Task Token_Success_False_Returns_Token_Response_Value()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(new TokenResponse(Rnd.Str, false));

			// Act
			var result = await tokenController.GetToken(tokenRequest);
			var unauthorizedResult = (UnauthorizedObjectResult)result;
			var tokenResponseViewModel = (TokenResponseViewModel)unauthorizedResult.Value!;

			// Assert
			Assert.False(tokenResponseViewModel.Success);
			Assert.True(string.IsNullOrWhiteSpace(tokenResponseViewModel.AccessToken));
			Assert.True(string.IsNullOrWhiteSpace(tokenResponseViewModel.RefreshToken));
		}

		[Fact]
		public async Task JwtTokenIssuer_GetTokenAsync_Is_Called_Once()
		{
			// Arrange
			var tokenController = new TokenController(
				jwtTokenIssuer,
				log);

			var tokenRequest = new TokenRequest(
				Rnd.Str,
				Rnd.Str);

			jwtTokenIssuer.GetTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.Returns(new TokenResponse(Rnd.Str, true));

			// Act
			await tokenController.GetToken(tokenRequest);

			// Assert
			await jwtTokenIssuer.Received(1).GetTokenAsync(Arg.Any<string>(), Arg.Any<string>());
		}
	}
}

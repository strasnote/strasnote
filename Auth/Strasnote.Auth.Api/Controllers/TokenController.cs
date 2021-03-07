using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Auth.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[controller]")]
	public class TokenController : Controller
	{
		private readonly IJwtToken jwtToken;
		private readonly ILog<TokenController> log;

		public record TokenRequest([Required] string Email, [Required] string Password);
		record TokenResponse(string AccessToken, string RefreshToken, string? Message, bool Success);

		public TokenController(IJwtToken jwtToken, ILog<TokenController> log) =>
			(this.jwtToken, this.log) = (jwtToken, log);

		[HttpPost]
		public async Task<IActionResult> GetToken(TokenRequest tokenRequest)
		{
			log.Trace("User {@User} logging in", tokenRequest.Email);
			var token = await jwtToken.GetTokenAsync(tokenRequest.Email, tokenRequest.Password);

			var tokenResponse = new TokenResponse(token.AccessToken, token.RefreshToken, token.Message, token.Success);

			if (!token.Success)
			{
				log.Trace("User {@User} failed to log in with message {@Message}", tokenRequest.Email, tokenResponse.Message ?? "Login failed");
				return Unauthorized(tokenResponse);
			}

			log.Trace("User {@User} logged in successfully", tokenRequest.Email);
			return Ok(tokenResponse);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> GetRefreshToken()
		{
			throw new NotImplementedException();
		}
	}
}

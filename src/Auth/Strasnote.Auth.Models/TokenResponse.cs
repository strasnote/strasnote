// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Auth.Models
{
	public record TokenResponse
	{
		/// <summary>
		/// Short-lived access token
		/// </summary>
		public string AccessToken { get; private init; }

		/// <summary>
		/// Message to display in response
		/// </summary>
		public string? Message { get; private init; }

		/// <summary>
		/// Long-lived refresh token
		/// </summary>
		public string RefreshToken { get; private init; }

		/// <summary>
		/// Whether or not the creation of the token was a success
		/// </summary>
		public bool Success { get; private init; }

		/// <summary>
		/// Returns a valid JWT login token
		/// </summary>
		/// <param name="accessToken">Access token</param>
		/// <param name="refreshToken">Refresh token</param>
		public TokenResponse(string accessToken, string refreshToken)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			Success = true;
		}

		/// <summary>
		/// Returns a response message and status, with empty tokens
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="success">The status</param>
		public TokenResponse(string message, bool success)
		{
			Message = message;
			Success = success;
			AccessToken = string.Empty;
			RefreshToken = string.Empty;
		}
	}
}

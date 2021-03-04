namespace Strasnote.Auth.Models
{
	/// <summary>
	/// Returns a valid JWT login token
	/// </summary>
	/// <param name="AccessToken">Primary access token</param>
	/// <param name="RefreshToken">Refresh token</param>
	public record TokenResponse(string AccessToken, string RefreshToken);
}

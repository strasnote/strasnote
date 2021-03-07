using System.ComponentModel.DataAnnotations;

namespace Strasnote.Auth.Config
{
	public class JwtConfig
	{
		/// <summary>
		/// The JWT secret.
		/// </summary>
		[Required]
		public string Secret { get; set; } = string.Empty;

		/// <summary>
		/// The default JWT access token expiry time.
		/// </summary>
		[Required]
		public int TokenExpiryMinutes { get; set; }

		/// <summary>
		/// The default JWT refresh token expiry time.
		/// </summary>
		[Required]
		public int RefreshTokenExpiryMinutes { get; set; }

		/// <summary>
		/// The domain of the JWT issuer.
		/// </summary>
		[Required]
		public string Issuer { get; set; } = string.Empty;

		/// <summary>
		/// The audience (domain) of the JWT issuer.
		/// </summary>
		[Required]
		public string Audience { get; set; } = string.Empty;
	}
}

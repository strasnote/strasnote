using System.ComponentModel.DataAnnotations;

namespace Strasnote.Auth.Config
{
	public class AuthConfig
	{
		/// <summary>
		/// The name of the section in appsettings.
		/// </summary>
		public const string AppSettingsSectionName = "Auth";

		/// <summary>
		/// The JWT configuration.
		/// </summary>
		[Required]
		public JwtConfig Jwt { get; set; } = new();
	}
}

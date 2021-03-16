// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
		/// The Database configuration.
		/// </summary>
		[Required]
		public DbConfig Db { get; set; } = new();

		/// <summary>
		/// The JWT configuration.
		/// </summary>
		[Required]
		public JwtConfig Jwt { get; set; } = new();
	}
}

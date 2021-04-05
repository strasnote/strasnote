// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Config
{
	/// <summary>
	/// Default user configuration
	/// </summary>
	public sealed class UserConfig
	{
		/// <summary>
		/// The name of the section in appsettings.
		/// </summary>
		public const string AppSettingsSectionName = "User";

		/// <summary>
		/// Default user's email address
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Default user's password
		/// </summary>
		public string? Password { get; set; }
	}
}

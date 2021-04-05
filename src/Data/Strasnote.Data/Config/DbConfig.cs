// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Config
{
	public sealed record DbConfig
	{
		/// <summary>
		/// The name of the section in appsettings.
		/// </summary>
		public const string AppSettingsSectionName = "Db";

		/// <summary>
		/// MySQL database configuration
		/// </summary>
		public MySqlDbConfig? MySql { get; init; }
	}
}

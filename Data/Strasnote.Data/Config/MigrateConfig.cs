// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Config
{
	public sealed class MigrateConfig
	{
		/// <summary>
		/// The name of the section in appsettings.
		/// </summary>
		public const string AppSettingsSectionName = "Migrate";

		/// <summary>
		/// If true, the database will be migrated to the latest version when the app starts
		/// </summary>
		public bool MigrateToLatestOnStartup { get; set; }

		/// <summary>
		/// If true, the database will be wiped clean, recreated, and test data inserted
		/// </summary>
		public bool NukeOnStartup { get; set; }
	}
}

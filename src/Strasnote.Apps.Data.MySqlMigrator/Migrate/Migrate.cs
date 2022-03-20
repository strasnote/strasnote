// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public static class Migrate
	{
		/// <summary>
		/// Migrate the database to a specific version
		/// </summary>
		/// <param name="client">ISqlClient</param>
		public static void Execute(ISqlClient client)
		{
			// Get version
			var version = GetVersion();

			Console.WriteLine("== Migrating to database version {0} == ", version);

			try
			{
				// Migrate to specified version and log to console
				client.MigrateTo(version);

				// Finished
				Console.WriteLine("Database successfully migrated to version {0}.", version);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error running database migration: {0}", ex);
			}
		}

		/// <summary>
		/// Get migration version
		/// </summary>
		static private long GetVersion()
		{
			// Ask for the version and attempt to parse
			Console.WriteLine("Please enter the migration version:");
			var typed = Console.ReadLine();

			if (long.TryParse(typed, out long version))
			{
				return version;
			}

			// Loop until we get a valid number
			Console.WriteLine("'{0}' is not a valid number.", typed);
			return GetVersion();
		}
	}
}

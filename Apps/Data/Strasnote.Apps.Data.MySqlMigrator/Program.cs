// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Clients.MySql;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	static internal class Program
	{
		static private void Main(string[] args)
		{
			Console.WriteLine("= Strasnote: MySQL Migrator =");

			bool loop = true;
			while (loop)
			{
				// Get the action
				Console.WriteLine("Please enter the action you would like to take:");
				Console.WriteLine("[up] [down] [exit]");
				switch (Console.ReadLine())
				{
					// Both up and down require a version number
					case "up":
					case "down":
						var version = GetVersion();
						MigrateTo(version);
						break;

					// Setting loop to false will stop the next iteration
					case "exit":
						loop = false;
						break;
				}
			}

			Console.WriteLine("Goodbye!");
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

		/// <summary>
		/// Migrate to the specified version
		/// </summary>
		/// <param name="version">Database version</param>
		static private void MigrateTo(long version)
		{
			Console.WriteLine("== Migrating to database version {0} == ", version);

			// Get connection string
			Console.WriteLine("Please paste your database connection string:");
			var connectionString = Console.ReadLine();
			if (connectionString is null)
			{
				Console.WriteLine("You must enter a connection string.");
				return;
			}

			try
			{
				// Create migrator
				var client = new MySqlDbClient(connectionString);

				// Migrate to specified version and log to console
				client.MigrateTo(version, new SimpleMigrations.Console.ConsoleLogger());

				// Finished
				Console.WriteLine("Database successfully migrated to version {0}.", version);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error running database migration: {0}", ex);
			}
		}
	}
}

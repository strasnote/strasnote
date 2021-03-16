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
			Console.WriteLine("Strasnote: MySQL Migrator");

			bool loop = true;
			while (loop)
			{
				Console.WriteLine("Please enter the action you would like to take:");
				Console.WriteLine("[up] [down] [exit]");

				switch (Console.ReadLine())
				{
					case "up":
					case "down":
						var version = GetVersion();
						MigrateTo(version);
						break;

					case "exit":
						loop = false;
						break;
				}
			}

			Console.WriteLine("Goodbye!");
		}

		static private long GetVersion()
		{
			Console.WriteLine("Please enter the migration version:");
			var typed = Console.ReadLine();

			if (long.TryParse(typed, out long version))
			{
				return version;
			}

			Console.WriteLine("'{0}' is not a valid number.", typed);
			return GetVersion();
		}

		static private void MigrateTo(long version)
		{
			Console.WriteLine("Migrating to database version {0}.", version);

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
				var client = new MySqlDbClient();

				// Migrate to specified version
				client.MigrateTo(version, connectionString, new SimpleMigrations.Console.ConsoleLogger());

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

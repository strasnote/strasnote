// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using MySql.Data.MySqlClient;
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
				Console.WriteLine("[migrate] [nuke] [exit]");
				switch (Console.ReadLine())
				{
					// Migrate requires a version number
					case "migrate":
						var version = GetVersion();
						MigrateTo(version);
						break;

					case "nuke":
						Nuke();
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
		/// Get the connection string
		/// </summary>
		static private string? GetConnectionString()
		{
			Console.WriteLine("Please paste your database connection string:");
			var connectionString = Console.ReadLine();
			if (connectionString is null)
			{
				Console.WriteLine("You must enter a connection string.");
				return null;
			}

			return connectionString;
		}

		/// <summary>
		/// Migrate to the specified version
		/// </summary>
		/// <param name="version">Database version</param>
		static private void MigrateTo(long version)
		{
			Console.WriteLine("== Migrating to database version {0} == ", version);

			var connectionString = GetConnectionString();
			if (connectionString is null)
			{
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

		/// <summary>
		/// Nuke the database tables and insert test data
		/// </summary>
		static private void Nuke()
		{
			// Make sure the user really wants to do this
			Console.WriteLine("== Nuking database == ");
			Console.WriteLine("Are you SURE you want to do this?");
			Console.WriteLine("** Everything ** will be deleted and test data recreated.");
			Console.WriteLine("Type 'yes' to proceed, anything else to quit.");
			if (Console.ReadLine()?.Trim().ToLower() != "yes")
			{
				return;
			}

			Console.WriteLine("Proceeding with nuke...");
			var connectionString = GetConnectionString();
			if (connectionString is null)
			{
				return;
			}

			try
			{
				// Create database client
				var client = new MySqlDbClient(connectionString);

				// Migrate to 0 (i.e. remove everything)
				Console.WriteLine("Removing all data and tables...");
				client.MigrateTo(0, new SimpleMigrations.Console.ConsoleLogger());

				// Migrate to latest
				Console.WriteLine("Migrating to latest version...");
				client.MigrateTo(version: null, new SimpleMigrations.Console.ConsoleLogger());

				// Insert test data
				Console.WriteLine("Inserting test data...");

				var connection = new MySqlConnection(connectionString);

				Console.Write("  users..");
				var userIds = TestData.Users(connection);
				Console.WriteLine("done");

				Console.Write("  roles..");
				var roleIds = TestData.Roles(connection);
				Console.WriteLine("done");

				Console.Write("  linking users to roles..");
				TestData.UserRoles(connection, userIds, roleIds);
				Console.WriteLine("done");

				// Done
				Console.WriteLine("Nuke complete.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error nuking database: {0}", ex);
			}
		}
	}
}

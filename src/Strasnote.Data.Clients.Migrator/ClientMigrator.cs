// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Data.Common;
using System.Reflection;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;
using Strasnote.Data.Clients.Enums;

namespace Strasnote.Data.Clients
{
	/// <summary>
	/// Migrator using SimpleMigrations
	/// </summary>
	public abstract class ClientMigrator
	{
		/// <summary>
		/// Migrate to the specified <paramref name="version"/> (or the latest if it's null)
		/// </summary>
		/// <param name="dbConnection">DbConnection</param>
		/// <param name="dbType">DbType</param>
		/// <param name="migrationsAssembly">The assembly from which to load migrations</param>
		/// <param name="version">The version to migrate to - if null will migrate to the latest version</param>
		static protected bool MigrateTo(DbConnection dbConnection, DbType dbType, Assembly migrationsAssembly, long? version)
		{
			// Get the correct provider
			var provider = dbType switch
			{
				DbType.MySql =>
					new MysqlDatabaseProvider(dbConnection),

				_ =>
					throw new Exception("Unknown dbType: " + dbType)
			};

			// Create the migrator
			var migrator = new SimpleMigrator(migrationsAssembly, provider, new ConsoleLogger());

			// Get all the migrations
			migrator.Load();

			// Migrate to specific version, or the latest version
			if (version is long specificVersion)
			{
				migrator.MigrateTo(specificVersion);
			}
			else
			{
				migrator.MigrateToLatest();
			}

			// Ensure the migration succeeded
			return migrator.LatestMigration.Version == version;
		}
	}
}

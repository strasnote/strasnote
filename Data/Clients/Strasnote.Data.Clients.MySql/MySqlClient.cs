// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Exceptions;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// MySQL database client
	/// </summary>
	public sealed class MySqlClient : ISqlClient
	{
		/// <inheritdoc/>
		public string ConnectionString { get; }

		/// <inheritdoc/>
		public IDbTables Tables { get; } = new DbTables();

		/// <inheritdoc/>
		public ISqlQueries Queries { get; } = new MySqlQueries();

		/// <summary>
		/// Inject and verify database configuration
		/// </summary>
		/// <param name="config">DbConfig</param>
		public MySqlClient(IOptions<DbConfig> config)
		{
			// Verify configuration
			if (config.Value.MySql is null)
			{
				throw new DbConfigMissingException<MySqlDbConfig>();
			}

			if (!config.Value.MySql.IsValid)
			{
				throw new DbConfigInvalidException<MySqlDbConfig>();
			}

			// Define connection string
			var mysql = config.Value.MySql;
			ConnectionString = string.Format(
				"server={0};port={1};user id={2};password={3};database={4};convert zero datetime=True;{5}",
				mysql.Host,
				mysql.Port,
				mysql.User,
				mysql.Pass,
				mysql.Database,
				mysql.Custom
			);
		}

		/// <summary>
		/// Define connection string manually
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		public MySqlClient(string connectionString) =>
			ConnectionString = connectionString;

		/// <inheritdoc/>
		public IDbConnection Connect() =>
			new MySqlConnection(ConnectionString);

		/// <inheritdoc/>
		public bool MigrateTo(long? version) =>
			MigrateTo(version, null);

		/// <summary>
		/// Perform database migration
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to - if not set the latest version will be used</param>
		/// <param name="logger">[Optional] Log output</param>
		public bool MigrateTo(long? version, ILogger? logger)
		{
			// Connection to database
			using var db = new MySqlConnection(ConnectionString);

			// Get migration objects
			var provider = new MysqlDatabaseProvider(db);
			var migrator = new SimpleMigrator(typeof(MySqlClient).Assembly, provider, logger);

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

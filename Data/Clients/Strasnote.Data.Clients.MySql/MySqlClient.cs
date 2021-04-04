// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Exceptions;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// MySQL database client
	/// </summary>
	public sealed class MySqlClient : ClientMigrator, ISqlClient
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
		public bool MigrateToLatest() =>
			MigrateTo(null);

		/// <inheritdoc/>
		public bool MigrateTo(long version) =>
			MigrateTo(version);

		/// <inheritdoc/>
		public void Nuke() =>
			MigrateTo(0);

		/// <summary>
		/// Perform database migration
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to - if not set the latest version will be used</param>
		private bool MigrateTo(long? version)
		{
			using var db = new MySqlConnection(ConnectionString);
			return MigrateTo(db, Enums.DbType.MySql, typeof(MySqlClient).Assembly, version);
		}
	}
}

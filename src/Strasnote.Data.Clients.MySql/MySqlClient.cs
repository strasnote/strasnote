﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MySqlConnector;
using MySqlConnector.Logging;
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
		/// <summary>
		/// Use Serilog for logging
		/// </summary>
		static MySqlClient() =>
			MySqlConnectorLogManager.Provider = new SerilogLoggerProvider();

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
		public async ValueTask<IDbConnection> ConnectAsync()
		{
			var connection = new MySqlConnection(ConnectionString);
			await connection.OpenAsync();
			return connection;
		}

		/// <inheritdoc/>
		public bool MigrateToLatest() =>
			MigrateToVersion(null);

		/// <inheritdoc/>
		public bool MigrateTo(long version) =>
			MigrateToVersion(version);

		/// <inheritdoc/>
		public void Nuke() =>
			MigrateToVersion(0);

		/// <summary>
		/// Perform database migration
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to - if not set the latest version will be used</param>
		private bool MigrateToVersion(long? version)
		{
			using var db = new MySqlConnection(ConnectionString);
			return MigrateTo(db, Enums.DbType.MySql, typeof(MySqlClient).Assembly, version);
		}
	}
}

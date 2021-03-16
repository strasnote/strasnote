using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// MySQL-compatible database client
	/// </summary>
	public sealed class MySqlDbClient : IDbClient
	{
		/// <inheritdoc/>
		public IDbConnection Connect(string connectionString) =>
			new MySqlConnection(connectionString);

		/// <inheritdoc/>
		public bool MigrateTo(long version, string connectionString)
		{
			// Connection to database
			using var db = new MySqlConnection(connectionString);

			// Get migration objects
			var provider = new MysqlDatabaseProvider(db);
			var migrator = new SimpleMigrator(typeof(MySqlDbClient).Assembly, provider);

			// Perform the migration
			migrator.Load();
			migrator.MigrateTo(version);

			// Ensure the migration succeeded
			return migrator.LatestMigration.Version == version;
		}
	}
}

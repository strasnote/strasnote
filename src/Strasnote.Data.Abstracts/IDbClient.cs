// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts database client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// Return the connection string for the client
		/// </summary>
		string ConnectionString { get; }

		/// <summary>
		/// Database-specific table names
		/// </summary>
		IDbTables Tables { get; }

		/// <summary>
		/// Perform database migration to the latest version
		/// </summary>
		bool MigrateToLatest();

		/// <summary>
		/// Perform database migration to specified <paramref name="version"/>
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to</param>
		bool MigrateTo(long version);

		/// <summary>
		/// Delete all data and tables
		/// </summary>
		void Nuke();
	}
}

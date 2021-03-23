// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts a database connection client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// Return the connection string for the client
		/// </summary>
		string ConnectionString { get; }

		/// <summary>
		/// Connect to the database
		/// </summary>
		IDbConnection Connect();

		/// <summary>
		/// Perform database migration to specified <paramref name="version"/>, or the latest available version
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to - if not set the latest version will be used</param>
		bool MigrateTo(long? version);
	}

	/// <inheritdoc cref="IDbClient"/>
	/// <typeparam name="TDbQueries">Db Queries type</typeparam>
	public interface IDbClientWithQueries : IDbClient
	{
		/// <summary>
		/// Retrieves database-specific queries
		/// </summary>
		IDbQueries Queries { get; }

		/// <summary>
		/// Database-specific table names
		/// </summary>
		IDbTables Tables { get; }
	}
}

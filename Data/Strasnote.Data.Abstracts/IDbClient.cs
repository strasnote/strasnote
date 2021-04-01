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
		/// Perform database migration to specified <paramref name="version"/>, or the latest available version
		/// </summary>
		/// <param name="version">[Optional] The version to migrate the database to - if not set the latest version will be used</param>
		bool MigrateTo(long? version);
	}
}

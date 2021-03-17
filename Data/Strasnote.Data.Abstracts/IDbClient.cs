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
		/// Perform database migration
		/// </summary>
		/// <param name="version">The version to migrate the database to</param>
		/// <param name="connectionString">Database connection string</param>
		bool MigrateTo(long version);
	}
}

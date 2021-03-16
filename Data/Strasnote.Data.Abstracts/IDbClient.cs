// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Microsoft.Extensions.Logging;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts a database connection client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// Connect to the database
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		IDbConnection Connect(string connectionString);

		/// <summary>
		/// Perform database migration
		/// </summary>
		/// <param name="version">The version to migrate the database to</param>
		/// <param name="connectionString">Database connection string</param>
		bool MigrateTo(long version, string connectionString);
	}
}

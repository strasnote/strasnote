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
		/// Connect to the database
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		IDbConnection Connect(string connectionString);

		/// <summary>
		/// Perform database migration
		/// </summary>
		bool MigrateTo(long version, string connectionString);
	}
}

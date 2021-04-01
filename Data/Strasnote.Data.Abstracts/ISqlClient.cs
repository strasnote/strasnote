// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts SQL database client
	/// </summary>
	public interface ISqlClient : IDbClient
	{
		/// <summary>
		/// Retrieves database-specific queries
		/// </summary>
		ISqlQueries Queries { get; }

		/// <summary>
		/// Connect to the database
		/// </summary>
		IDbConnection Connect();
	}
}

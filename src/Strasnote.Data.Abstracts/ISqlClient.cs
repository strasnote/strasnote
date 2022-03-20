// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using System.Threading.Tasks;

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
		/// Create and open a database connection
		/// </summary>
		ValueTask<IDbConnection> ConnectAsync();
	}
}

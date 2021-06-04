// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;

namespace Strasnote.Data.Abstracts
{
	public interface ISqlQueries
	{
		/// <summary>
		/// String to select all columns
		/// </summary>
		string SelectAll { get; }

		/// <summary>
		/// Return Create query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns</param>
		string GetCreateQuery(string table, List<string> columns);

		/// <summary>
		/// Return Retrieve (by ID) query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="entityId">Entity ID value</param>
		/// <param name="userId">Optional User ID to retrieve items relating to the specified user only</param>
		string GetRetrieveQuery(string table, List<string> columns, long entityId, long? userId);

		/// <summary>
		/// Return Retrieve query using all predicates (performs an AND query)
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">List of predicates (uses AND)</param>
		/// <param name="userId">Optional User ID to retrieve items relating to the specified user only</param>
		(string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table,
			List<string> columns, List<(string column, SearchOperator op, object value)> predicates,
			long? userId
		);

		/// <summary>
		/// Return Update query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="entityId">Entity ID value</param>
		/// <param name="userId">Optional User ID to retrieve items relating to the specified user only</param>
		string GetUpdateQuery(string table, List<string> columns, long entityId, long? userId);

		/// <summary>
		/// Return Delete query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="entityId">Entity ID value</param>
		/// <param name="userId">Optional User ID to retrieve items relating to the specified user only</param>
		string GetDeleteQuery(string table, long entityId, long? userId);
	}
}

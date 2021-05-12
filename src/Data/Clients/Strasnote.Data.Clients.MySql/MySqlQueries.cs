// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Sec;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// MySQL compatible database queries
	/// </summary>
	public sealed class MySqlQueries : ISqlQueries
	{
		/// <inheritdoc/>
		public string SelectAll =>
			"*";

		/// <inheritdoc/>
		public string GetCreateQuery(string table, List<string> columns)
		{
			// Get columns
			var col = new List<string>();
			var par = new List<string>();
			foreach (var column in columns)
			{
				if (column == nameof(IEntity.Id))
				{
					continue;
				}

				col.Add($"`{column}`");
				par.Add($"@{column}");
			}

			// Return query
			return $"INSERT INTO `{table}` ({string.Join(", ", col)}) VALUES ({string.Join(", ", par)}); SELECT LAST_INSERT_ID();";
		}

		/// <inheritdoc/>
		public string GetRetrieveQuery(string table, List<string> columns, string idColumn, long id)
		{
			// Get columns
			string select;

			if (columns.Count > 0)
			{
				var col = new List<string>();
				foreach (var column in columns)
				{
					col.Add($"`{column}`");
				}

				select = string.Join(", ", col);
			}
			else
			{
				select = SelectAll;
			}

			// Return query
			return $"SELECT {select} FROM `{table}` WHERE `{idColumn}` = {id};";
		}

		/// <inheritdoc/>
		public (string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table, List<string> columns, List<(string column, SearchOperator op, object value)> predicates
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column}`");
			}

			// Add each predicate to the where and parameter lists
			var where = new List<string>();
			var param = new Dictionary<string, object>();
			var index = 0;
			foreach (var (column, op, value) in predicates)
			{
				var parameter = $"P{index++}";

				where.Add($"`{column}` {op.ToOperator()} @{parameter}");
				param.Add(parameter, value);
			}

			// Return query and parameters
			return ($"SELECT {string.Join(", ", col)} FROM `{table}` WHERE {string.Join(" AND ", where)};", param);
		}

		/// <inheritdoc/>
		public string GetUpdateQuery(string table, List<string> columns, string idColumn, long id)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				if (column == nameof(IEntity.Id))
				{
					continue;
				}

				col.Add($"`{column}` = @{column}");
			}

			// Return query
			return $"UPDATE `{table}` SET {string.Join(", ", col)} WHERE `{idColumn}` = {id};";
		}

		/// <inheritdoc/>
		public string GetDeleteQuery(string table, string idColumn, long id) =>
			$"DELETE FROM `{table}` WHERE `{idColumn}` = {id};";
	}
}

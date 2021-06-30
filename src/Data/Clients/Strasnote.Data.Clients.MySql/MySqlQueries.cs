// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections;
using System.Collections.Generic;
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
		public string GetRetrieveQuery(string table, List<string> columns, ulong entityId, ulong? userId)
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

			// Build query
			var sql = $"SELECT {select} FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId}";

			// Add User ID
			if (userId is not null)
			{
				sql += $" AND `{nameof(IEntityWithUserId.UserId)}` = {userId}";
			}

			// Return
			return $"{sql};";
		}

		/// <inheritdoc/>
		public (string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table,
			List<string> columns, List<(string column, SearchOperator op, object value)> predicates,
			ulong? userId
		)
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

			// Add User ID
			if (userId is not null)
			{
				predicates.Add((nameof(IEntityWithUserId.UserId), SearchOperator.Equal, userId));
			}

			// Add each predicate to the where and parameter lists
			var where = new List<string>();
			var param = new Dictionary<string, object>();
			var index = 0;
			foreach (var (column, op, value) in predicates)
			{
				// Add using standard operators
				if (op != SearchOperator.In && op != SearchOperator.NotIn)
				{
					var parameter = $"P{index++}";

					where.Add($"`{column}` {op.ToOperator()} @{parameter}");
					param.Add(parameter, value);
				}
				// IN operator requires a list (string is a special case that implements IEnumerable<char>)
				else if (value is not string && value is IEnumerable list)
				{
					// Add a parameter for each value in the list
					var inParameters = new List<string>();
					foreach (var inValue in list)
					{
						var inParameter = $"@P{index++}";

						param.Add(inParameter, inValue);
						inParameters.Add(inParameter);
					}

					// If there are any parameters, add them
					if (inParameters.Count > 0)
					{
						where.Add($"`{column}` {op.ToOperator()} ({string.Join(',', inParameters)})");
					}
				}
			}

			// Return query and parameters
			return ($"SELECT {select} FROM `{table}` WHERE {string.Join(" AND ", where)};", param);
		}

		/// <inheritdoc/>
		public string GetUpdateQuery(string table, List<string> columns, ulong entityId, ulong? userId)
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

			// Build query
			var sql = $"UPDATE `{table}` SET {string.Join(", ", col)} WHERE `{nameof(IEntity.Id)}` = {entityId}";

			// Add User ID
			if (userId is not null)
			{
				sql += $" AND `{nameof(IEntityWithUserId.UserId)}` = {userId}";
			}

			// Return
			return $"{sql};";
		}

		/// <inheritdoc/>
		public string GetDeleteQuery(string table, ulong entityId, ulong? userId)
		{
			// Build query
			var sql = $"DELETE FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId}";

			// Add User ID
			if (userId is not null)
			{
				sql += $" AND `{nameof(IEntityWithUserId.UserId)}` = {userId}";
			}

			// Return
			return $"{sql};";
		}
	}
}

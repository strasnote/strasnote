// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlQueries_Tests
{
	public class GetCreateQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Insert_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var expected = $"INSERT INTO `{table}` (`{c0}`, `{c1}`, `{c2}`) VALUES (@{c0}, @{c1}, @{c2}); " +
				"SELECT LAST_INSERT_ID();";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetCreateQuery(table, columns);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Ignores_Id_Column()
		{
			// Arrange
			var table = Rnd.Str;

			const string? c0 = nameof(IEntity.Id);
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var expected = $"INSERT INTO `{table}` (`{c1}`, `{c2}`) VALUES (@{c1}, @{c2}); " +
				"SELECT LAST_INSERT_ID();";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetCreateQuery(table, columns);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

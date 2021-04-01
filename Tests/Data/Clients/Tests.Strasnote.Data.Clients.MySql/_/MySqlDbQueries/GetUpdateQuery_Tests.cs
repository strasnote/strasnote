// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlDbQueries_Tests
{
	public class GetUpdateQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Update_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var idColumn = Rnd.Str;
			var id = Rnd.Int;

			var expected = $"UPDATE `{table}` SET `{c0}` = @{c0}, `{c1}` = @{c1}, `{c2}` = @{c2} " +
				$"WHERE `{idColumn}` = {id};";

			var queries = new MySqlDbQueries();

			// Act
			var result = queries.GetUpdateQuery(table, columns, idColumn, id);

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

			var idColumn = Rnd.Str;
			var id = Rnd.Int;

			var expected = $"UPDATE `{table}` SET `{c1}` = @{c1}, `{c2}` = @{c2} " +
				$"WHERE `{idColumn}` = {id};";

			var queries = new MySqlDbQueries();

			// Act
			var result = queries.GetUpdateQuery(table, columns, idColumn, id);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

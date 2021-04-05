// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlQueries_Tests
{
	public class GetDeleteQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Delete_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var idColumn = Rnd.Str;
			var id = Rnd.Lng;

			var expected = $"DELETE FROM `{table}` WHERE `{idColumn}` = {id};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetDeleteQuery(table, idColumn, id);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

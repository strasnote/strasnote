// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
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
			var entityId = Rnd.Lng;

			var expected = $"DELETE FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetDeleteQuery(table, entityId, null);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Valid_Delete_Query_With_UserId()
		{
			// Arrange
			var table = Rnd.Str;
			var entityId = Rnd.Lng;
			var userId = Rnd.Lng;

			var expected = $"DELETE FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId} " +
				$"AND `{nameof(IEntityWithUserId.UserId)}` = {userId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetDeleteQuery(table, entityId, userId);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

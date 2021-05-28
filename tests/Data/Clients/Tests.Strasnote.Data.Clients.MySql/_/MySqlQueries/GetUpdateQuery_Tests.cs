// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlQueries_Tests
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

			var entityId = Rnd.Lng;

			var expected = $"UPDATE `{table}` SET `{c0}` = @{c0}, `{c1}` = @{c1}, `{c2}` = @{c2} " +
				$"WHERE `{nameof(IEntity.Id)}` = {entityId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetUpdateQuery(table, columns, entityId, null);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Valid_Update_Query_With_UserId()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var entityId = Rnd.Lng;
			var userId = Rnd.Lng;

			var expected = $"UPDATE `{table}` SET `{c0}` = @{c0}, `{c1}` = @{c1}, `{c2}` = @{c2} " +
				$"WHERE `{nameof(IEntity.Id)}` = {entityId} AND `{nameof(IEntityWithUserId.UserId)}` = {userId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetUpdateQuery(table, columns, entityId, userId);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Ignores_Id_Column()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var columns = new List<string>(new[] { nameof(IEntity.Id), c0, c1 });

			var entityId = Rnd.Lng;

			var expected = $"UPDATE `{table}` SET `{c0}` = @{c0}, `{c1}` = @{c1} " +
				$"WHERE `{nameof(IEntity.Id)}` = {entityId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetUpdateQuery(table, columns, entityId, null);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

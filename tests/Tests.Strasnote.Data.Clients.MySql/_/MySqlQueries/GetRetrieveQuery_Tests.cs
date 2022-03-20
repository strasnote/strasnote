// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql.MySqlQueries_Tests
{
	public class GetRetrieveQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Select_By_Id_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var entityId = Rnd.Ulng;

			var expected = $"SELECT `{c0}`, `{c1}`, `{c2}` FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetRetrieveQuery(table, columns, entityId, null);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Valid_Select_By_Id_Query_With_UserId()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var entityId = Rnd.Ulng;
			var userId = Rnd.Ulng;

			var expected = $"SELECT `{c0}`, `{c1}`, `{c2}` FROM `{table}` WHERE `{nameof(IEntity.Id)}` = {entityId} " +
				$"AND `{nameof(IEntityWithUserId.UserId)}` = {userId};";

			var queries = new MySqlQueries();

			// Act
			var result = queries.GetRetrieveQuery(table, columns, entityId, userId);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Valid_Select_With_Predicates_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var p0Column = Rnd.Str;
			const SearchOperator p0Operator = SearchOperator.Like;
			var p0Value = Rnd.Str;

			var p1Column = Rnd.Str;
			const SearchOperator p1Operator = SearchOperator.MoreThanOrEqual;
			var p1Value = Rnd.Int;

			var predicates = new List<(string, SearchOperator, object)>
			{
				{ ( p0Column, p0Operator, p0Value ) },
				{ ( p1Column, p1Operator, p1Value ) }
			};

			var queries = new MySqlQueries();

			var expected = $"SELECT `{c0}`, `{c1}`, `{c2}` " +
				$"FROM `{table}` WHERE `{p0Column}` LIKE @P0 AND `{p1Column}` >= @P1;";

			// Act
			var (query, param) = queries.GetRetrieveQuery(table, columns, predicates, null);

			// Assert
			Assert.Equal(expected, query);
			Assert.Collection(param,
				x =>
				{
					Assert.Equal("P0", x.Key);
					Assert.Equal(p0Value, x.Value);
				},
				x =>
				{
					Assert.Equal("P1", x.Key);
					Assert.Equal(p1Value, x.Value);
				}
			);
		}

		[Fact]
		public void Returns_Valid_Select_With_Predicates_And_UserId_Query()
		{
			// Arrange
			var table = Rnd.Str;

			var c0 = Rnd.Str;
			var c1 = Rnd.Str;
			var c2 = Rnd.Str;
			var columns = new List<string>(new[] { c0, c1, c2 });

			var p0Column = Rnd.Str;
			const SearchOperator p0Operator = SearchOperator.Like;
			var p0Value = Rnd.Str;

			var p1Column = Rnd.Str;
			const SearchOperator p1Operator = SearchOperator.MoreThanOrEqual;
			var p1Value = Rnd.Int;

			var predicates = new List<(string, SearchOperator, object)>
			{
				{ ( p0Column, p0Operator, p0Value ) },
				{ ( p1Column, p1Operator, p1Value ) }
			};

			var userId = Rnd.Ulng;

			var queries = new MySqlQueries();

			var expected = $"SELECT `{c0}`, `{c1}`, `{c2}` " +
				$"FROM `{table}` WHERE `{p0Column}` LIKE @P0 AND `{p1Column}` >= @P1 " +
				$"AND `{nameof(IEntityWithUserId.UserId)}` = @P2;";

			// Act
			var (query, param) = queries.GetRetrieveQuery(table, columns, predicates, userId);

			// Assert
			Assert.Equal(expected, query);
			Assert.Collection(param,
				x =>
				{
					Assert.Equal("P0", x.Key);
					Assert.Equal(p0Value, x.Value);
				},
				x =>
				{
					Assert.Equal("P1", x.Key);
					Assert.Equal(p1Value, x.Value);
				},
				x =>
				{
					Assert.Equal("P2", x.Key);
					Assert.Equal(userId, x.Value);
				}
			);
		}
	}
}

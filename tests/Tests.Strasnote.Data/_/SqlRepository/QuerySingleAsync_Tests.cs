// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Linq.Expressions;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.SqlRepository_Tests
{
	public class QuerySingleAsync_Tests
	{
		[Fact]
		public void Calls_Get_Retrieve_Query_With_Correct_Values()
		{
			// Arrange
			var (repo, _, queries, _, table) = SqlRepository_Setup.Get();

			const SearchOperator p0Operator = SearchOperator.Like;
			object p0Value = Rnd.Str;

			const SearchOperator p1Operator = SearchOperator.MoreThanOrEqual;
			object p1Value = Rnd.Int;

			var predicates = new (Expression<Func<TestEntity, object>>, SearchOperator, object)[]
			{
				( e => e.Foo, p0Operator, p0Value ),
				( e => e.Bar, p1Operator, p1Value )
			};

			var userId = Rnd.Ulng;

			// Act
			_ = repo.QuerySingleAsync<TestEntity>(userId, predicates);

			// Assert
			queries.Received().GetRetrieveQuery(table, Arg.Is<List<string>>(c =>
				c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
			), Arg.Is<List<(string, SearchOperator, object)>>(p =>
				p[0].Item1 == nameof(TestEntity.Foo) && p[0].Item2 == p0Operator && p[0].Item3 == p0Value
				&& p[1].Item1 == nameof(TestEntity.Bar) && p[1].Item2 == p1Operator && p[1].Item3 == p1Value
			), userId);
		}

		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (repo, _, _, log, _) = SqlRepository_Setup.Get();
			var predicates = new (Expression<Func<TestEntity, object>>, SearchOperator, object)[]
			{
				( e => e.Foo, SearchOperator.Like, Rnd.Str )
			};

			var userId = Rnd.Ulng;

			// Act
			_ = repo.QuerySingleAsync<TestEntity>(userId, predicates);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

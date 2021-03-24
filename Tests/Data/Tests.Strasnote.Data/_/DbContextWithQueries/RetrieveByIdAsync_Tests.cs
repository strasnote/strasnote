// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public class RetrieveByIdAsync_Tests
	{
		[Fact]
		public void Calls_Get_Retrieve_Query_With_Correct_Values()
		{
			// Arrange
			var (context, _, queries, _, table) = DbContextWithQueries.GetContext();
			var id = Rnd.Lng;

			// Act
			context.RetrieveByIdAsync<TestEntity>(id);

			// Assert
			queries.Received().GetRetrieveQuery(table, Arg.Is<List<string>>(c =>
				c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
			), nameof(IEntity.Id), id);
		}

		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (context, _, _, log, _) = DbContextWithQueries.GetContext();

			// Act
			context.RetrieveByIdAsync<TestEntity>(Rnd.Lng);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public class UpdateAsync_Tests
	{
		[Fact]
		public void Calls_Get_Update_Query_With_Correct_Values()
		{
			// Arrange
			var (context, _, queries, _, table) = DbContextWithQueries.GetContext();
			var id = Rnd.Lng;
			var entity = new TestEntity(id, Rnd.Str, Rnd.Int);

			// Act
			context.UpdateAsync<TestEntity>(entity);

			// Assert
			queries.Received().GetUpdateQuery(table, Arg.Is<List<string>>(c =>
				c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
			), nameof(IEntity.Id), id);
		}

		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (context, _, _, log, _) = DbContextWithQueries.GetContext();
			var entity = new TestEntity(0, Rnd.Str, Rnd.Int);

			// Act
			context.UpdateAsync<TestEntity>(entity);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

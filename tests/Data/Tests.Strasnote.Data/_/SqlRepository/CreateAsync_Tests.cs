// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.SqlRepository_Tests
{
	public class CreateAsync_Tests
	{
		[Fact]
		public async Task Calls_Get_Create_Query_With_Correct_Values()
		{
			// Arrange
			var (repo, _, queries, _, table) = SqlRepository_Setup.Get();
			var entity = new TestEntity(0, Rnd.Str, Rnd.Int);

			// Act
			await repo.CreateAsync(entity);

			// Assert
			queries.Received().GetCreateQuery(table, Arg.Is<List<string>>(c =>
				c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
			));
		}

		[Fact]
		public async Task Logs_Operation()
		{
			// Arrange
			var (repo, _, _, log, _) = SqlRepository_Setup.Get();
			var entity = new TestEntity(0, Rnd.Str, Rnd.Int);

			// Act
			await repo.CreateAsync(entity);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

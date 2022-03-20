// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.SqlRepository_Tests
{
	public class RetrieveAsync_Tests
	{
		[Fact]
		public void Calls_Get_Retrieve_Query_With_Correct_Values()
		{
			// Arrange
			var (repo, _, queries, _, table) = SqlRepository_Setup.Get();
			var entityId = Rnd.Ulng;
			var userId = Rnd.Ulng;

			// Act
			_ = repo.RetrieveAsync<TestEntity>(entityId, userId);

			// Assert
			queries.Received().GetRetrieveQuery(table, Arg.Is<List<string>>(c =>
				c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
			), entityId, userId);
		}

		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (repo, _, _, log, _) = SqlRepository_Setup.Get();

			// Act
			_ = repo.RetrieveAsync<TestEntity>(Rnd.Ulng, Rnd.Ulng);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

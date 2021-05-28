// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.SqlRepository_Tests
{
	public class UpdateAsync_Tests
	{
		// TODO: 
		// Because UpdateAsync does the update and then retrieves the updated value,
		// and Dapper is an extension method for IDbConnection, testing is tricky / not possible

		//[Fact]
		//public async Task Calls_Get_Update_Query_With_Correct_Values()
		//{
		//	// Arrange
		//	var (repo, _, queries, _, table) = SqlRepository_Setup.Get();
		//	var id = Rnd.Lng;
		//	var entity = new TestEntity(id, Rnd.Str, Rnd.Int);

		//	// Act
		//	await repo.UpdateAsync(id, entity).ConfigureAwait(false);

		//	// Assert
		//	queries.Received().GetUpdateQuery(table, Arg.Is<List<string>>(c =>
		//		c[0] == nameof(TestEntity.Bar) && c[1] == nameof(TestEntity.Foo) && c[2] == nameof(TestEntity.Id)
		//	), nameof(IEntity.Id), id);
		//}

		//[Fact]
		//public async Task Logs_Operation()
		//{
		//	// Arrange
		//	var (repo, _, _, log, _) = SqlRepository_Setup.Get();
		//	var entity = new TestEntity(0, Rnd.Str, Rnd.Int);

		//	// Act
		//	await repo.UpdateAsync(0, entity).ConfigureAwait(false);

		//	// Assert
		//	log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		//}
	}
}

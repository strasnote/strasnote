// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.SqlRepository_Tests
{
	public class DeleteAsync_Tests
	{
		[Fact]
		public async Task Calls_Get_Delete_Query_With_Correct_Values()
		{
			// Arrange
			var (repo, _, queries, _, table) = SqlRepository_Setup.Get();
			var entityId = Rnd.Ulng;
			var userId = Rnd.Ulng;

			// Act
			await repo.DeleteAsync(entityId, userId);

			// Assert
			queries.Received().GetDeleteQuery(table, entityId, userId);
		}

		[Fact]
		public async Task Logs_Operation()
		{
			// Arrange
			var (repo, _, _, log, _) = SqlRepository_Setup.Get();

			// Act
			await repo.DeleteAsync(Rnd.Ulng, Rnd.Ulng);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

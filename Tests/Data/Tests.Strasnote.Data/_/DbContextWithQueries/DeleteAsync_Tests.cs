// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public class DeleteAsync_Tests
	{
		[Fact]
		public void Calls_Get_Delete_Query_With_Correct_Values()
		{
			// Arrange
			var (context, _, queries, _, table) = DbContextWithQueries.GetContext();
			var id = Rnd.Lng;

			// Act
			context.DeleteAsync(id);

			// Assert
			queries.Received().GetDeleteQuery(table, nameof(IEntity.Id), id);
		}

		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (context, _, _, log, _) = DbContextWithQueries.GetContext();

			// Act
			context.DeleteAsync(Rnd.Lng);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

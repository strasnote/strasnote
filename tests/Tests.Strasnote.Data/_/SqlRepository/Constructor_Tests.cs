// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.SqlRepository_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Log_Property()
		{
			// Arrange
			var (_, client, _, log, table) = SqlRepository_Setup.Get();

			// Act
			var result = new TestRepository(client, log, table);

			// Assert
			Assert.Same(log, result.LogTest);
		}

		[Fact]
		public void Sets_Queries_Property()
		{
			// Arrange
			var (_, client, queries, log, table) = SqlRepository_Setup.Get();

			// Act
			var result = new TestRepository(client, log, table);

			// Assert
			Assert.Same(queries, result.QueriesTest);
		}

		[Fact]
		public void Sets_Table_Property()
		{
			// Arrange
			var (_, client, _, log, table) = SqlRepository_Setup.Get();

			// Act
			var result = new TestRepository(client, log, table);

			// Assert
			Assert.Same(table, result.TableTest);
		}
	}
}

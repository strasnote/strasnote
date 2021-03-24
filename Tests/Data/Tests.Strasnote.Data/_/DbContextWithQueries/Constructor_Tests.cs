// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Xunit;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Calls_Client_Connect()
		{
			// Arrange
			var (_, client, _, log, table) = DbContextWithQueries.GetContext();

			// Act
			var _ = new TestDbContext(client, log, table);

			// Assert
			client.Received().Connect();
		}

		[Fact]
		public void Sets_Log_Property()
		{
			// Arrange
			var (_, client, _, log, table) = DbContextWithQueries.GetContext();

			// Act
			var result = new TestDbContext(client, log, table);

			// Assert
			Assert.Same(log, result.LogTest);
		}

		[Fact]
		public void Sets_Queries_Property()
		{
			// Arrange
			var (_, client, queries, log, table) = DbContextWithQueries.GetContext();

			// Act
			var result = new TestDbContext(client, log, table);

			// Assert
			Assert.Same(queries, result.QueriesTest);
		}

		[Fact]
		public void Sets_Table_Property()
		{
			// Arrange
			var (_, client, _, log, table) = DbContextWithQueries.GetContext();

			// Act
			var result = new TestDbContext(client, log, table);

			// Assert
			Assert.Same(table, result.TableTest);
		}
	}
}

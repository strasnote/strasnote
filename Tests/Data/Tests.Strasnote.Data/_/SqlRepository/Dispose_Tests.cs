// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.SqlRepository_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Disposes_Connection()
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			var client = Substitute.For<ISqlClient>();
			client.Connect().Returns(connection);
			var log = Substitute.For<ILog>();
			var repo = new TestDbContext(client, log);

			// Act
			repo.Dispose();

			// Assert
			connection.Received().Dispose();
		}

		[Fact]
		public void Disposes_Connection_Only_Once()
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			var client = Substitute.For<ISqlClient>();
			client.Connect().Returns(connection);
			var log = Substitute.For<ILog>();
			var repo = new TestDbContext(client, log);

			// Act
			repo.Dispose();
			repo.Dispose();

			// Assert
			connection.Received(1).Dispose();
		}

		public sealed class TestDbContext : SqlRepository<TestEntity>
		{
			public TestDbContext(ISqlClient client, ILog log) : base(client, log, Rnd.Str) { }
		}

		public sealed record TestEntity(long Id) : IEntity;
	}
}

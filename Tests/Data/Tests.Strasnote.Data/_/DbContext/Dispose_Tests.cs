// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Xunit;

namespace Strasnote.Data.DbContext_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Disposes_Connection()
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			var client = Substitute.For<IDbClient>();
			client.Connect().Returns(connection);
			var log = Substitute.For<ILog>();
			var context = new TestDbContext(client, log);

			// Act
			context.Dispose();

			// Assert
			connection.Received().Dispose();
		}

		[Fact]
		public void Disposes_Connection_Only_Once()
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			var client = Substitute.For<IDbClient>();
			client.Connect().Returns(connection);
			var log = Substitute.For<ILog>();
			var context = new TestDbContext(client, log);

			// Act
			context.Dispose();
			context.Dispose();

			// Assert
			connection.Received(1).Dispose();
		}

		public sealed class TestDbContext : DbContext<TestEntity>
		{
			public TestDbContext(IDbClient client, ILog log) : base(client, log) { }
		}

		public sealed record TestEntity(long Id) : IEntity;
	}
}

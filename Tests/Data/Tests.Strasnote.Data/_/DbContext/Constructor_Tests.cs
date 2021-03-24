// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Xunit;

namespace Strasnote.Data.DbContext_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Calls_Client_Connect()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();

			// Act
			var _ = new TestDbContext(client, log);

			// Assert
			client.Received().Connect();
		}

		[Fact]
		public void Sets_Log_Property()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();

			// Act
			var result = new TestDbContext(client, log);

			// Assert
			Assert.Same(log, result.Log);
		}

		public sealed class TestDbContext : DbContext<TestEntity>
		{
			public TestDbContext(IDbClient client, ILog log) : base(client, log) { }
		}

		public sealed record TestEntity(long Id) : IEntity;
	}
}

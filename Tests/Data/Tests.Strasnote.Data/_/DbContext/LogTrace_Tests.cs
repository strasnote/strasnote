// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContext_Tests
{
	public class LogTrace_Tests
	{
		static private (TestDbContext, ILog) GetContext()
		{
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();
			return (new TestDbContext(client, log), log);
		}

		[Fact]
		public void Calls_Log_Trace()
		{
			// Arrange
			var (context, log) = GetContext();
			var message = Rnd.Str;
			var args = new object[] { Rnd.Int, Rnd.Int };

			// Act
			context.LogTraceTest(message, args);

			// Assert
			log.Received().Trace(message, args);
		}

		public sealed class TestDbContext : DbContext<TestEntity>
		{
			public TestDbContext(IDbClient client, ILog log) : base(client, log) { }
		}

		public sealed record TestEntity(long Id) : IEntity;
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;

namespace Strasnote.Data.DbContext_Tests
{
	public static class DbContext_Setup
	{
		static internal (TestDbContext, IDbClient, IDbQueries, ILog, string) GetContext()
		{
			var queries = Substitute.For<IDbQueries>();
			queries.SelectAll.Returns("*");
			var client = Substitute.For<IDbClient>();
			client.Queries.Returns(queries);
			var log = Substitute.For<ILog>();
			var table = Rnd.Str;
			return (new TestDbContext(client, log, table), client, queries, log, table);
		}
	}

	public sealed class TestDbContext : DbContext<TestEntity>
	{
		public TestDbContext(IDbClient client, ILog log, string table) : base(client, log, table) { }
	}

	public sealed record TestEntity(long Id, string Foo, int Bar) : IEntity
	{
		[Ignore]
		public bool AlwaysIgnore { get; set; }
	}
}

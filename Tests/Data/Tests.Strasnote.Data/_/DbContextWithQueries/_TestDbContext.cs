// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public static class DbContextWithQueries
	{
		static internal (TestDbContext, IDbClientWithQueries, IDbQueries, ILog, string) GetContext()
		{
			var queries = Substitute.For<IDbQueries>();
			queries.SelectAll.Returns("*");
			var client = Substitute.For<IDbClientWithQueries>();
			client.Queries.Returns(queries);
			var log = Substitute.For<ILog>();
			var table = Rnd.Str;
			return (new TestDbContext(client, log, table), client, queries, log, table);
		}
	}

	public sealed class TestDbContext : DbContextWithQueries<TestEntity>
	{
		public TestDbContext(IDbClientWithQueries client, ILog log, string table) : base(client, log, table) { }
	}

	public sealed record TestEntity(long Id, string Foo, int Bar) : IEntity
	{
		[Ignore]
		public bool AlwaysIgnore { get; set; }
	}
}

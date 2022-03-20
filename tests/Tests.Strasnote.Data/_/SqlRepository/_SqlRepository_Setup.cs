// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Data.SqlRepository_Tests
{
	public static class SqlRepository_Setup
	{
		static internal (TestRepository, ISqlClient, ISqlQueries, ILog, string) Get()
		{
			var queries = Substitute.For<ISqlQueries>();
			queries.SelectAll.Returns("*");
			var client = Substitute.For<ISqlClient>();
			client.Queries.Returns(queries);
			var log = Substitute.For<ILog>();
			var table = Rnd.Str;
			return (new TestRepository(client, log, table), client, queries, log, table);
		}
	}

	public sealed class TestRepository : SqlRepository<TestEntity>
	{
		public TestRepository(ISqlClient client, ILog log, string table) : base(client, log, table) { }
	}

	public sealed record TestEntity(ulong Id, string Foo, int Bar) : IEntity
	{
		[Ignore]
		public bool AlwaysIgnore { get; set; }
	}
}

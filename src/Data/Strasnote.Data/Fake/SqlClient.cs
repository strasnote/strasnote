// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Fake
{
	public sealed class SqlClient : ISqlClient
	{
		public string ConnectionString =>
			string.Empty;

		public ISqlQueries Queries =>
			throw new System.NotImplementedException();

		public IDbTables Tables =>
			throw new System.NotImplementedException();

		public IDbConnection Connect() =>
			new DbConnection();

		public bool MigrateTo(long? version) =>
			true;
		public bool MigrateTo(long version) => throw new System.NotImplementedException();
		public bool MigrateToLatest() => throw new System.NotImplementedException();
		public void Nuke() => throw new System.NotImplementedException();
	}
}

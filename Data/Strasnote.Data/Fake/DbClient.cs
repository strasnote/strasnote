// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Fake
{
	public sealed class DbClient : IDbClient
	{
		public string ConnectionString =>
			string.Empty;

		public IDbConnection Connect() =>
			new DbConnection();

		public bool MigrateTo(long? version) =>
			true;
	}
}

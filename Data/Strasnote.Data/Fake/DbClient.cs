// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Fake
{
	public sealed class DbClient : IDbClient
	{
		public IDbConnection Connect(string connectionString) =>
			new DbConnection();

		public bool MigrateTo(long version, string connectionString) =>
			true;
	}
}

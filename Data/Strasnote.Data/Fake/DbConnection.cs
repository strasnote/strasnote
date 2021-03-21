// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Data;
using System.Data.Common;

namespace Strasnote.Data.Fake
{
	public sealed class DbConnection : System.Data.Common.DbConnection
	{
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
		public override string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

		public override string Database => throw new NotImplementedException();

		public override string DataSource => throw new NotImplementedException();

		public override string ServerVersion => throw new NotImplementedException();

		public override ConnectionState State => throw new NotImplementedException();

		public override void ChangeDatabase(string databaseName) => throw new NotImplementedException();
		public override void Close() => throw new NotImplementedException();
		public override void Open() => throw new NotImplementedException();
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => throw new NotImplementedException();
		protected override DbCommand CreateDbCommand() => throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
	}
}

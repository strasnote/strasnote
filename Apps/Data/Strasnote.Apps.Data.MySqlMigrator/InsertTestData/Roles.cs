// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public static partial class InsertTestData
	{
		public static List<int> Roles(MySqlConnection connection)
		{
			var r0 = new RoleEntity
			{
				Name = "Role 0",
				NormalizedName = "role-0",
				ConcurrencyStamp = Rnd.RndString.Get(16)
			};

			var r1 = new RoleEntity
			{
				Name = "Role 1",
				NormalizedName = "role-1",
				ConcurrencyStamp = Rnd.RndString.Get(16)
			};

			const string? sql = "INSERT " +
				"INTO `auth.role` (`Name`, `NormalizedName`, `ConcurrencyStamp`) " +
				"VALUES (@Name, @NormalizedName, @ConcurrencyStamp);" +
				"SELECT LAST_INSERT_ID();";

			var r0Id = connection.ExecuteScalar<int>(sql, r0);
			Console.Write("{0} ", r0Id);

			var r1Id = connection.ExecuteScalar<int>(sql, r1);
			Console.Write("{0} ", r1Id);

			return new List<int>
			{
				{ r0Id },
				{ r1Id }
			};
		}
	}
}

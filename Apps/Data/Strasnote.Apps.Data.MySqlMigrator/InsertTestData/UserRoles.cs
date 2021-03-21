// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public static partial class InsertTestData
	{
		public static void UserRoles(MySqlConnection connection, List<int> userIds, List<int> roleIds)
		{
			foreach (var user in userIds)
			{
				const string? sql = "INSERT " +
					"INTO `auth.user_role` (`UserId`, `RoleId`) " +
					"VALUES (@UserId, @RoleId);" +
					"SELECT LAST_INSERT_ID();";

				foreach (var role in roleIds)
				{
					var id = connection.ExecuteScalar<int>(sql, new UserRoleEntity { UserId = user, RoleId = role });
					Console.Write("{0} ", id);
				}
			}
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using Strasnote.Data.Entities.Auth;
using Strasnote.Encryption;
using Strasnote.Util;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public static partial class InsertTestData
	{
		public static List<int> Users(MySqlConnection connection)
		{
			var u0email = "craig@nykindale.co.uk";
			var u0pwd = "fred";
			var u0keys = Keys.Generate(u0pwd).Unwrap(() => throw new Exception("Unable to get keys."));
			var u0 = new UserEntity
			{
				UserName = u0email,
				NormalizedUserName = u0email,
				PasswordHash = Hash.PasswordArgon(u0pwd).Unwrap(() => throw new Exception("Unable to get password.")),
				Email = u0email,
				NormalizedEmail = u0email,
				UserPublicKey = u0keys.PublicKey,
				UserPrivateKey = u0keys.PrivateKey,
				ConcurrencyStamp = Rnd.RndString.Get(16),
				SecurityStamp = Rnd.RndString.Get(16)
			};

			var u1email = "ben@bcgdesign.com";
			var u1pwd = "fred";
			var u1keys = Keys.Generate(u1pwd).Unwrap(() => throw new Exception("Unable to get keys."));
			var u1 = new UserEntity
			{
				UserName = u1email,
				NormalizedUserName = u1email,
				PasswordHash = Hash.PasswordArgon(u0pwd).Unwrap(() => throw new Exception("Unable to get password.")),
				Email = u1email,
				NormalizedEmail = u1email,
				EmailConfirmed = true,
				UserPublicKey = u1keys.PublicKey,
				UserPrivateKey = u1keys.PrivateKey,
				ConcurrencyStamp = Rnd.RndString.Get(16),
				SecurityStamp = Rnd.RndString.Get(16)
			};

			const string? sql = "INSERT " +
				"INTO `auth.user` (`UserName`, `NormalizedUserName`, `PasswordHash`, `Email`, `NormalizedEmail`, `UserPublicKey`, `UserPrivateKey`, `ConcurrencyStamp`, `SecurityStamp`) " +
				"VALUES (@UserName, @NormalizedUserName, @PasswordHash, @Email, @NormalizedEmail, @UserPublicKey, @UserPrivateKey, @ConcurrencyStamp, @SecurityStamp);" +
				"SELECT LAST_INSERT_ID();";

			var u0Id = connection.ExecuteScalar<int>(sql, u0);
			Console.Write("{0} ", u0Id);

			var u1Id = connection.ExecuteScalar<int>(sql, u1);
			Console.Write("{0} ", u1Id);

			return new List<int>
			{
				{ u0Id },
				{ u1Id }
			};
		}
	}
}

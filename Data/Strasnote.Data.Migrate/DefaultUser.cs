// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Jeebs;
using Jeebs.Linq;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Encryption;
using Strasnote.Logging;
using static F.OptionF;

namespace Strasnote.Data.Migrate
{
	/// <summary>
	/// Create the default user
	/// </summary>
	public static class DefaultUser
	{
		/// <summary>
		/// Insert the default user from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">IUserRepository</param>
		/// <param name="config">UserConfig</param>
		public static void Insert(ILog log, IUserRepository repo, UserConfig config)
		{
			var user = from ep in GetEmailAndPassword(config)
					   from keys in Keys.Generate(ep.password)
					   select new UserEntity
					   {
						   UserName = ep.email,
						   NormalizedUserName = ep.email,
						   Email = ep.email,
						   NormalizedEmail = ep.email,
						   EmailConfirmed = true,
						   PasswordHash = ep.password,
						   PhoneNumber = string.Empty,
						   UserPublicKey = keys.PublicKey,
						   UserPrivateKey = keys.PrivateKey,
						   SecurityStamp = Guid.NewGuid().ToString(),
						   ConcurrencyStamp = Guid.NewGuid().ToString()
					   };

			user.Switch(
				some: async x => await repo.CreateAsync(x).ConfigureAwait(false),
				none: r => log.Error("Unable to create user: {Reason}", r)
			);
		}

		/// <summary>
		/// Check email and password are both set before returning them
		/// </summary>
		/// <param name="config">UserConfig</param>
		static internal Option<(string email, string password)> GetEmailAndPassword(UserConfig config)
		{
			if (config.Email is string email && config.Password is string password)
			{
				var hasher = new PasswordHasher<UserEntity>();
				var hashed = hasher.HashPassword(new UserEntity(), password);

				return Return(
					(email, hashed)
				);
			}

			return None<(string, string), Msg.EmailAndPasswordMustBothBeSetMsg>();
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Email and password must both be set to automatically create a user</summary>
			public sealed record EmailAndPasswordMustBothBeSetMsg : IMsg { }
		}
	}
}

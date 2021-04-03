// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Encryption;
using Strasnote.Util;

namespace Strasnote.Data.Migrate
{
	public static class DefaultUser
	{
		public static async Task InsertAsync(ISqlRepository<UserEntity> repo, UserConfig config)
		{
			if (config.Email is string email && config.Password is string password)
			{
				foreach (var hash in Hash.PasswordArgon(password))
				{
					foreach (var keys in Keys.Generate(password))
					{
						var user = new UserEntity
						{
							UserName = email,
							NormalizedUserName = email,
							Email = email,
							NormalizedEmail = email,
							EmailConfirmed = true,
							PasswordHash = hash,
							PhoneNumber = string.Empty,
							UserPublicKey = keys.PublicKey,
							UserPrivateKey = keys.PrivateKey,
							SecurityStamp = Rnd.RndString.Get(16),
							ConcurrencyStamp = Rnd.RndString.Get(16),
							UserProfile = "{}"
						};

						await repo.CreateAsync<long>(user).ConfigureAwait(false);
					}
				}
			}
		}
	}
}

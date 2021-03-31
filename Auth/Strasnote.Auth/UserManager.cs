// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth
{
	public sealed class UserManager : UserManager<UserEntity>, IUserManager
	{
		public UserManager(
			IUserStore<UserEntity> store,
			Microsoft.Extensions.Options.IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<UserEntity> passwordHasher,
			IEnumerable<IUserValidator<UserEntity>> userValidators,
			IEnumerable<IPasswordValidator<UserEntity>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			IServiceProvider services,
			Microsoft.Extensions.Logging.ILogger<UserManager<UserEntity>> logger) : base(
				store,
				optionsAccessor,
				passwordHasher,
				userValidators,
				passwordValidators,
				keyNormalizer,
				errors,
				services,
				logger)
		{
		}
	}
}

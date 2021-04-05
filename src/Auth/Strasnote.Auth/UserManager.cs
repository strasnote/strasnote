// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Strasnote.Auth.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth
{
	public sealed class UserManager : UserManager<UserEntity>, IUserManager
	{
		public UserManager(
			IUserStore<UserEntity> store,
			IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<UserEntity> passwordHasher,
			IEnumerable<IUserValidator<UserEntity>> userValidators,
			IEnumerable<IPasswordValidator<UserEntity>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			IServiceProvider services,
			ILogger<UserManager<UserEntity>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
		}
	}
}

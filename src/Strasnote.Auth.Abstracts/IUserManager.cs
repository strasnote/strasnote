// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Abstracts
{
	public interface IUserManager
	{
		Task<UserEntity> FindByEmailAsync(string email);

		Task<UserEntity> FindByIdAsync(string userId);

		Task<IList<string>> GetRolesAsync(UserEntity user);

		IPasswordHasher<UserEntity> PasswordHasher { get; }
	}
}

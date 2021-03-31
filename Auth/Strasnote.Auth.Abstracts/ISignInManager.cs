// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Abstracts
{
	public interface ISignInManager
	{
		Task<SignInResult> CheckPasswordSignInAsync(UserEntity user, string password, bool lockoutOnFailure);
	}
}

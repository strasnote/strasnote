// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Strasnote.Auth.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth
{
	public sealed class SignInManager : SignInManager<UserEntity>, ISignInManager
	{
		public SignInManager(
			UserManager<UserEntity> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<UserEntity> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<SignInManager<UserEntity>> logger,
			IAuthenticationSchemeProvider schemes,
			IUserConfirmation<UserEntity> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
		}
	}
}

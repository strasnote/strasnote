// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth
{
	public sealed class SignInManager : SignInManager<UserEntity>, ISignInManager
	{
		public SignInManager(
			UserManager<UserEntity> userManager,
			Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<UserEntity> claimsFactory,
			Microsoft.Extensions.Options.IOptions<IdentityOptions> optionsAccessor,
			Microsoft.Extensions.Logging.ILogger<SignInManager<UserEntity>> logger,
			Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes) : base(
				userManager,
				contextAccessor,
				claimsFactory,
				optionsAccessor,
				logger,
				schemes)
		{
		}
	}
}

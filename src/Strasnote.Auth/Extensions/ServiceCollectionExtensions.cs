// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Config;

namespace Strasnote.Auth.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register the auth services
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="configuration">IConfiguration</param>
		public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
		{
			// JWT Tokens
			services.AddTransient<IJwtTokenIssuer, JwtTokenIssuer>();
			services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
			services.AddTransient<JwtSecurityTokenHandler>();

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(config =>
				{
					config.RequireHttpsMetadata = true;
					config.SaveToken = true;
					config.TokenValidationParameters = new AuthConfigTokenValidationParameters(configuration);
				});

			services.AddAuthorization();

			// Identity
			services.AddTransient<IUserManager, UserManager>();
			services.AddTransient<ISignInManager, SignInManager>();

			return services;
		}
	}
}

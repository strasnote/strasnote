// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
		/// <returns>The <paramref name="services"/> object with the added services.</returns>
		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			// JWT Tokens
			services.AddTransient<IJwtTokenIssuer, JwtTokenIssuer>();

			// Identity
			services.AddTransient<IUserManager, UserManager>();
			services.AddTransient<ISignInManager, SignInManager>();

			// Config options
			services.AddOptions<AuthConfig>()
				.Bind(configuration.GetSection(AuthConfig.AppSettingsSectionName))
				.ValidateDataAnnotations(); // ToDo: implement validation output/log (startup?): https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0#options-validation

			return services;
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register the auth data services
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		public static IServiceCollection AddAuthData<TClient>(this IServiceCollection services)
			where TClient : class, ISqlClient
		{
			// Add database client
			services.AddTransient<ISqlClient, TClient>();
			services.AddTransient<IDbClient>(s => s.GetRequiredService<ISqlClient>());

			// Add repositories
			services.AddTransient<IUserRepository, UserSqlRepository>();
			services.AddTransient<IUserStore<UserEntity>, UserStore>();

			services.AddTransient<IRefreshTokenRepository, RefreshTokenSqlRepository>();

			// Add identity services
			services
				.AddIdentityCore<UserEntity>(options =>
				{
					options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
				})
				.AddDefaultTokenProviders();

			return services;
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.DependencyInjection;
using Strasnote.Auth.Data.Abstracts;

namespace Strasnote.Auth.Data.Fake.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register the fake data services
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <returns>The <paramref name="services"/> object with the added services.</returns>
		public static IServiceCollection AddAuthDataFakeServices(this IServiceCollection services)
		{
			services.AddTransient<IRefreshTokenContext, RefreshTokenContext>();
			services.AddTransient<IRoleContext, RoleContext>();
			services.AddTransient<IUserContext, UserContext>();

			return services;
		}
	}
}

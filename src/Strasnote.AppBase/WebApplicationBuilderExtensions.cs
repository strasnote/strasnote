// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.AppBase.Abstracts;
using Strasnote.Data.TypeHandlers;

namespace Strasnote.AppBase
{
	/// <summary>
	/// <see cref="WebApplicationBuilder"/> extensions
	/// </summary>
	public static class WebApplicationBuilderExtensions
	{
		/// <summary>
		/// Add default configuration for Strasnote web apps
		/// </summary>
		/// <param name="builder">WebApplicationBuilder</param>
		public static WebApplicationBuilder ConfigureStrasnote(this WebApplicationBuilder builder) =>
			ConfigureStrasnote(builder, (_, _) => { });

		/// <summary>
		/// Add default configuration for Strasnote web apps, with additional configuration options
		/// for services and the application
		/// </summary>
		/// <param name="builder">WebApplicationBuilder</param>
		/// <param name="configure">Register custom services</param>
		public static WebApplicationBuilder ConfigureStrasnote(
			this WebApplicationBuilder builder,
			Action<IConfiguration, IServiceCollection> configure
		)
		{
			/** Add default Strasnote configuration
			 * ========================================================================== */
			_ = builder.Host.ConfigureStrasnote();

			/** Add default services
			 * ========================================================================== */
			_ = builder.Services

				// Enable custom Http Clients
				.AddHttpContextAccessor()

				// Add app context
				.AddTransient<IAppContext, WebAppContext>();

			/** Add custom services
			 * ========================================================================== */
			configure(builder.Configuration, builder.Services);

			/** Add Dapper TypeHandlers
			 * ========================================================================== */
			SqlMapper.AddTypeHandler(new DateTimeTypeHandler());

			/** Return
			 * ========================================================================== */
			return builder;
		}
	}
}

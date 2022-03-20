// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Strasnote.Auth.Config;
using Strasnote.Data.Config;
using Strasnote.Logging;

namespace Strasnote.AppBase
{
	/// <summary>
	/// <see cref="HostBuilder"/> extensions
	/// </summary>
	public static class HostBuilderExtensions
	{
		/// <summary>
		/// Add default configuration for Strasnote apps
		/// </summary>
		/// <param name="builder">IHostBuilder</param>
		public static IHostBuilder ConfigureStrasnote(this IHostBuilder builder) =>
			ConfigureStrasnote(builder, (_, _) => { });

		/// <summary>
		/// Add default configuration for Strasnote apps<br/>
		/// Application configuration is taken from (in order):<br/>
		///		- from JSON files<br/>
		///		- from Environment Variables<br/>
		///		- from Command Line
		/// </summary>
		/// <param name="builder">IHostBuilder</param>
		/// <param name="configureServices">Register custom services</param>
		public static IHostBuilder ConfigureStrasnote(
			this IHostBuilder builder,
			Action<HostBuilderContext, IServiceCollection> configureServices
		)
		{
			/** Load configuration secrets
			 * ========================================================================== */
			_ = builder.ConfigureAppConfiguration((ctx, config) =>
			{
				_ = config
					.AddJsonFile($"{ctx.HostingEnvironment.ContentRootPath}/appsettings.Secrets.json", optional: true);
			});

			/** Add default services
			 * ========================================================================== */
			_ = builder.ConfigureServices((ctx, services) =>
			{
				// Add logging
				_ = services
					.AddSingleton<ILog>(new SerilogLogger())
					.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));

				// Bind options
				// ToDo: implement validation output/log (startup?): https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0#options-validation
				_ = services
					.AddOptions<AuthConfig>()
					.Bind(ctx.Configuration.GetSection(AuthConfig.AppSettingsSectionName))
					.ValidateDataAnnotations();

				_ = services
					.AddOptions<DbConfig>()
					.Bind(ctx.Configuration.GetSection(DbConfig.AppSettingsSectionName))
					.ValidateDataAnnotations();

				_ = services
					.AddOptions<MigrateConfig>()
					.Bind(ctx.Configuration.GetSection(MigrateConfig.AppSettingsSectionName))
					.ValidateDataAnnotations();

				_ = services
					.AddOptions<UserConfig>()
					.Bind(ctx.Configuration.GetSection(UserConfig.AppSettingsSectionName))
					.ValidateDataAnnotations();

				// Add custom services configuration
				configureServices(ctx, services);
			});

			/** Register Serilog
			 * ========================================================================== */
			_ = builder.UseSerilog((ctx, logger) =>
			{
				_ = logger
					// Load from configuration values
					.ReadFrom.Configuration(ctx.Configuration)

					// Enrich with application name
					.Enrich.FromLogContext()
					.Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName);
			});

			/** Return
			 * ========================================================================== */
			return builder;
		}
	}
}

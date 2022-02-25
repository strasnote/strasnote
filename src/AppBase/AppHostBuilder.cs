// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
	/// App Host builder
	/// </summary>
	public abstract class AppHostBuilder
	{
		/// <inheritdoc/>
		public IHost Build(string[] args) =>
			GetBuilder(args).Build();

		/// <summary>
		/// Configure the host
		/// </summary>
		/// <param name="args">Command Line arguments</param>
		protected abstract IHostBuilder GetBuilder(string[] args);
	}

	/// <inheritdoc cref="AppHostBuilder"/>
	/// <typeparam name="THost">IHostEnvironment type</typeparam>
	public abstract class AppHostBuilder<THost> : AppHostBuilder
		where THost : IHostEnvironment
	{
		/// <summary>
		/// Configure Host
		/// </summary>
		/// <param name="config">IConfigurationBuilder</param>
		/// <param name="args">Command Line arguments</param>
		protected virtual void ConfigureHostConfiguration(IConfigurationBuilder config, string[] args)
		{
			// e.g. add command line
		}

		/// <summary>
		/// Configure App
		/// </summary>
		/// <param name="host">IHostEnvironment</param>
		/// <param name="config">IConfigurationBuilder</param>
		protected virtual void ConfigureAppConfiguration(IHostEnvironment host, IConfigurationBuilder config)
		{
			// Add JSON secrets
			config.AddJsonFile($"{host.ContentRootPath}/appsettings.Secrets.json", optional: true);

			// Add environment variables
			config.AddEnvironmentVariables();
		}

		/// <summary>
		/// Configure Serilog
		/// </summary>
		/// <param name="host">IHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		protected virtual void ConfigureSerilog(IHostEnvironment host, IConfiguration config, LoggerConfiguration loggerConfig) =>
			loggerConfig
				.ReadFrom.Configuration(config)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", host.ApplicationName);

		/// <summary>
		/// Configure Services
		/// </summary>
		/// <param name="host">IHostEnvironment</param>
		/// <param name="services">IServiceCollection</param>
		/// <param name="config">IConfiguration</param>
		protected virtual void ConfigureServices(THost host, IServiceCollection services, IConfiguration config)
		{
			// Add logging
			services.AddSingleton<ILog>(new SerilogLogger());
			services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));

			// Bind options
			// ToDo: implement validation output/log (startup?): https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0#options-validation
			services.AddOptions<AuthConfig>()
				.Bind(config.GetSection(AuthConfig.AppSettingsSectionName))
				.ValidateDataAnnotations();

			services.AddOptions<DbConfig>()
				.Bind(config.GetSection(DbConfig.AppSettingsSectionName))
				.ValidateDataAnnotations();

			services.AddOptions<MigrateConfig>()
				.Bind(config.GetSection(MigrateConfig.AppSettingsSectionName))
				.ValidateDataAnnotations();

			services.AddOptions<UserConfig>()
				.Bind(config.GetSection(UserConfig.AppSettingsSectionName))
				.ValidateDataAnnotations();
		}
	}
}

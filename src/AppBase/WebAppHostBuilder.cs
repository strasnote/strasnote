// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.AppBase
{
	/// <summary>
	/// Web App Host builder
	/// </summary>
	public abstract class WebAppHostBuilder : AppHostBuilder<IWebHostEnvironment>
	{
		/// <inheritdoc/>
		protected override IHostBuilder GetBuilder(string[] args) =>

			// Create default builder
			Host.CreateDefaultBuilder(
				args
			)

			// Configure Host
			.ConfigureHostConfiguration(
				config => ConfigureHostConfiguration(config, args)
			)

			// Configure App
			.ConfigureAppConfiguration(
				(host, config) => ConfigureAppConfiguration(host.HostingEnvironment, config)
			)

			// Configure Serilog
			.UseSerilog(
				(host, config) => ConfigureSerilog(host.HostingEnvironment, host.Configuration, config)
			)

			// Configure Web Host with defaults
			.ConfigureWebHostDefaults(builder => builder

				// Configure Services
				.ConfigureServices(
					(host, services) => ConfigureServices(host.HostingEnvironment, services, host.Configuration)
				)

				// Configure the Web Application
				.Configure(
					(host, app) => Configure(app, host.HostingEnvironment, host.Configuration)
				)

				// Force .NET to look for Controllers in the App assembly, not this one
				.UseSetting(
					WebHostDefaults.ApplicationKey, GetType().Assembly.FullName
				)
			);

		/// <summary>
		/// Configure Web Application
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		/// <param name="env">IWebHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		protected abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config);

		/// <inheritdoc/>
		protected override void ConfigureServices(IWebHostEnvironment host, IServiceCollection services, IConfiguration config)
		{
			base.ConfigureServices(host, services, config);

			services.AddHttpContextAccessor();

			services.AddTransient<IAppContext, WebAppContext>();
		}
	}
}

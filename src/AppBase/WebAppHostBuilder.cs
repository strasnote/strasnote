// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

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

			// Configure Web Host with defaults
			.ConfigureWebHostDefaults(builder => builder

				// Configure App
				.ConfigureAppConfiguration(
					(host, config) => ConfigureAppConfiguration(host.HostingEnvironment, config)
				)

				// Configure Serilog
				.UseSerilog(
					(host, config) => ConfigureSerilog(host.HostingEnvironment, host.Configuration, config)
				)

				// Configure Services
				.ConfigureServices(
					(host, services) => ConfigureServices(host.HostingEnvironment, services, host.Configuration)
				)

				// Configure the Web Application
				.Configure(
					(host, app) => Configure(app, host.HostingEnvironment, host.Configuration)
				)
			);

		/// <summary>
		/// Configure Web Application
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		/// <param name="env">IWebHostEnvironment</param>
		/// <param name="config">IConfiguration</param>
		protected abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config);
	}
}

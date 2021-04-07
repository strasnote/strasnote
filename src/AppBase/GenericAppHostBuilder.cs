// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.Hosting;
using Serilog;

namespace Strasnote.AppBase
{
	/// <summary>
	/// Generic App Host builder
	/// </summary>
	public abstract class GenericAppHostBuilder : AppHostBuilder<IHostEnvironment>
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

			// Configure Services
			.ConfigureServices(
				(host, services) => ConfigureServices(host.HostingEnvironment, services, host.Configuration)
			);
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Strasnote.Data.Migrate;
using Strasnote.Logging;

namespace Strasnote.Auth.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			// Enable basic console logging
			ConsoleLog.Enable();

			// Create host
			var host = CreateHostBuilder(args).Build();

			// Do database migration if required
			var migrator = new Migrator(host.Services);
			await migrator.ExecuteAsync();

			// Run application
			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseLogging()
				.ConfigureAppConfiguration((host, config) =>
				{
					config.AddSettings(host.HostingEnvironment);
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Strasnote.Logging;

namespace Strasnote.Auth.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ConsoleLog.Enable();
			CreateHostBuilder(args).Build().Run();
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

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.Data.Migrate;
using Strasnote.Logging;

namespace Strasnote.AppBase
{
	/// <summary>
	/// Run a program using <see cref="HostBuilder"/>
	/// </summary>
	public abstract class Program
	{
		/// <summary>
		/// Program entry point
		/// </summary>
		/// <typeparam name="T">Host Builder type</typeparam>
		/// <param name="args">Commmand Line arguments</param>
		/// <param name="app">[Optional] App to run asynchronously</param>
		public static async Task MainAsync<T>(string[] args, Func<IHost, IServiceProvider, Task>? app = null)
			where T : AppHostBuilder, new()
		{
			// Enable basic console logging - will be overridden by IAppHostBuilder
			ConsoleLog.Enable();

			// Create builder
			var hostBuilder = new T();

			// Build host
			using var host = hostBuilder.Build(args);

			// Ready to go
			var log = host.Services.GetRequiredService<ILog<T>>();
			log.Information("Application ready.");

			// Run migration
			var migrator = new Migrator(host.Services);
			await migrator.ExecuteAsync().ConfigureAwait(false);

			// Run app
			if (app is null)
			{
				await host.RunAsync().ConfigureAwait(false);
			}
			else
			{
				var provider = host.Services;
				await app(host, provider).ConfigureAwait(false);
			}
		}
	}
}

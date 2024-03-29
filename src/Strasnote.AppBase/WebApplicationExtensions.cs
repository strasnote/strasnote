﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Migrate;
using Strasnote.Logging;

namespace Strasnote.AppBase
{
	/// <summary>
	/// <see cref="WebApplication"/> extensions
	/// </summary>
	public static class WebApplicationExtensions
	{
		/// <summary>
		/// Use default application configuration
		/// </summary>
		/// <param name="app">WebApplication</param>
		public static WebApplication UseAppDefaults(this WebApplication app)
		{
			// Add development exception handling
			if (app.Environment.IsDevelopment())
			{
				_ = app.UseDeveloperExceptionPage();
			}

			// Redirect to HTTPS
			_ = app.UseHttpsRedirection();

			// Enable static files
			_ = app.UseStaticFiles();

			// Enable endpoint routing
			_ = app.UseRouting();

			// Add auth
			_ = app.UseAuthentication();
			_ = app.UseAuthorization();

			// Return
			return app;
		}

		/// <summary>
		/// Output application name and run database migration before running the app itself
		/// </summary>
		/// <param name="app">WebApplication</param>
		/// <param name="useData">If true, the database will be </param>
		public static async Task RunAppAsync(this WebApplication app)
		{
			// Ready to go
			var log = app.Services.GetRequiredService<ILog<int>>();
			log.Information("Application ready.");

			// Run migration
			if (app.Services.GetService<IDbClient>() is not null)
			{
				var migrator = new Migrator(app.Services);
				await migrator.ExecuteAsync().ConfigureAwait(false);
			}

			// Run app
			await app.RunAsync();
		}
	}
}

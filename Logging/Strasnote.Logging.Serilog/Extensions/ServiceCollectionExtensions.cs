// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Strasnote.Logging
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Add Strasnote standard logging to the application
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="configuration">IConfiguration</param>
		public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
		{
			// Override default console logger using configuration
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.CreateLogger();

			// Register logger classes
			services.AddSingleton<ILog>(new SerilogLogger(Log.Logger));
			services.AddTransient(typeof(ILog<>), typeof(SerilogLogger<>));

			return services;
		}
	}
}

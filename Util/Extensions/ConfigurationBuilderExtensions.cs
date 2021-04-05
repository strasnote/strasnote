// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Strasnote
{
	/// <summary>
	/// Extension methods for IConfigurationBuilder
	/// </summary>
	public static class ConfigurationBuilderExtensions
	{
		/// <summary>
		/// Add Secrets and environment settings
		/// </summary>
		/// <param name="this">IConfigurationBuilder</param>
		/// <param name="host">IHostEnvironment</param>
		public static IConfigurationBuilder AddSettings(this IConfigurationBuilder @this, IHostEnvironment host)
		{
			// Add secrets
			@this
				.AddJsonFile($"{host.ContentRootPath}/appsettings.Secrets.json", optional: true);

			// Add environment settings
			@this
				.AddJsonFile($"{host.ContentRootPath}/appsettings.{host.EnvironmentName}.json", optional: true);

			// Add environment variables
			@this.AddEnvironmentVariables();

			// Return
			return @this;
		}
	}
}

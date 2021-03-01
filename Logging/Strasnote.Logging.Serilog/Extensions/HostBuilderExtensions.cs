using Microsoft.Extensions.Hosting;
using Serilog;

namespace Strasnote.Logging
{
	public static class HostBuilderExtensions
	{
		/// <summary>
		/// Enable Strasnote standard logging using Serilog
		/// </summary>
		/// <param name="builder">IHostBuilder</param>
		public static IHostBuilder UseLogging(this IHostBuilder builder) =>
			builder.UseSerilog();
	}
}

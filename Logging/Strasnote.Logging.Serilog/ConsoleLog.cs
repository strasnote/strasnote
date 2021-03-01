using Serilog;
using Serilog.Events;

namespace Strasnote.Logging
{
	/// <summary>
	/// Serilog Console Log
	/// </summary>
	public static class ConsoleLog
	{
		/// <summary>
		/// Enable basic console logging using Serilog
		/// </summary>
		public static void Enable() =>
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				.WriteTo.Console()
				.CreateLogger();
	}
}

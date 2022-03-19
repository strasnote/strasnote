// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
		/// Create a basic console logger
		/// </summary>
		public static ILogger CreateConsoleLogger =>
			new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
				.WriteTo.Console()
				.CreateLogger();

		/// <summary>
		/// Enable basic console logging using Serilog
		/// </summary>
		public static void Enable() =>
			Log.Logger = CreateConsoleLogger;
	}
}

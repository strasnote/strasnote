using System;
using Serilog;
using Serilog.Events;

namespace Strasnote.Logging
{
	/// <summary>
	/// Contextual Serilog Logging
	/// </summary>
	/// <typeparam name="T">Context Type</typeparam>
	public class SerilogLogger<T> : SerilogLogger, ILog<T>
	{
		/// <summary>
		/// Create a contextual logger
		/// </summary>
		public SerilogLogger() : base(Log.Logger.ForContext<T>()) { }
	}

	/// <summary>
	/// Serilog Logging
	/// </summary>
	public class SerilogLogger : ILog
	{
		private readonly ILogger serilog;

		/// <summary>
		/// Prefix added to all messages
		/// </summary>
		public const string Prefix = "Strasnote | ";

		/// <summary>
		/// Use global logger
		/// </summary>
		public SerilogLogger() : this(Log.Logger) { }

		/// <summary>
		/// Use specified logger
		/// </summary>
		/// <param name="serilog">Serilog.ILogger instance</param>
		internal SerilogLogger(ILogger serilog) =>
			this.serilog = serilog;

		/// <inheritdoc/>
		public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level) =>
			serilog.IsEnabled((LogEventLevel)level);

		/// <inheritdoc/>
		public void Trace(string message, params object[] args) =>
			serilog.Verbose(Prefix + message, args);

		/// <inheritdoc/>
		public void Debug(string message, params object[] args) =>
			serilog.Debug(Prefix + message, args);

		/// <inheritdoc/>
		public void Information(string message, params object[] args) =>
			serilog.Information(Prefix + message, args);

		/// <inheritdoc/>
		public void Warning(string message, params object[] args) =>
			serilog.Warning(Prefix + message, args);

		/// <inheritdoc/>
		public void Error(string message, params object[] args) =>
			serilog.Error(Prefix + message, args);

		/// <inheritdoc/>
		public void Error(Exception ex, string message, params object[] args) =>
			serilog.Error(ex, Prefix + message, args);

		/// <inheritdoc/>
		public void Critical(string message, params object[] args) =>
			serilog.Fatal(Prefix + message, args);

		/// <inheritdoc/>
		public void Critical(Exception ex, string message, params object[] args) =>
			serilog.Fatal(ex, Prefix + message, args);
	}
}

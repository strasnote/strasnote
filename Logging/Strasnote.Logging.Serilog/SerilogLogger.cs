﻿using System;
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
		private readonly ILogger logger;

		/// <summary>
		/// Use global logger
		/// </summary>
		public SerilogLogger() : this(Log.Logger) { }

		/// <summary>
		/// Use specified logger
		/// </summary>
		/// <param name="logger">Serilog.ILogger instance</param>
		internal SerilogLogger(ILogger logger) =>
			this.logger = logger;

		/// <inheritdoc/>
		public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel level) =>
			logger.IsEnabled((LogEventLevel)level);

		/// <inheritdoc/>
		public void Trace(string message, params object[] args) =>
			logger.Verbose(message, args);

		/// <inheritdoc/>
		public void Debug(string message, params object[] args) =>
			logger.Debug(message, args);

		/// <inheritdoc/>
		public void Information(string message, params object[] args) =>
			logger.Information(message, args);

		/// <inheritdoc/>
		public void Warning(string message, params object[] args) =>
			logger.Warning(message, args);

		/// <inheritdoc/>
		public void Error(string message, params object[] args) =>
			logger.Error(message, args);

		/// <inheritdoc/>
		public void Error(Exception ex, string message, params object[] args) =>
			logger.Error(ex, message, args);

		/// <inheritdoc/>
		public void Critical(string message, params object[] args) =>
			logger.Fatal(message, args);

		/// <inheritdoc/>
		public void Critical(Exception ex, string message, params object[] args) =>
			logger.Fatal(ex, message, args);
	}
}
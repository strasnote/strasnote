// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.Logging;

namespace Strasnote.Logging
{
	/// <summary>
	/// Context-aware log interface
	/// </summary>
	/// <typeparam name="T">Context type</typeparam>
	public interface ILog<T> : ILog { }

	/// <summary>
	/// Log interface
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Returns true if logging is enabled for the specified level
		/// </summary>
		/// <param name="level">LogLevel to check</param>
		bool IsEnabled(LogLevel level);

		/// <summary>
		/// Write a trace log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Trace(string message, params object[] args);

		/// <summary>
		/// Write a debug log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Debug(string message, params object[] args);

		/// <summary>
		/// Write an information log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Information(string message, params object[] args);

		/// <summary>
		/// Write a warning log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Warning(string message, params object[] args);

		/// <summary>
		/// Write an error log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Error(string message, params object[] args);

		/// <summary>
		/// Write an error log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Error(Exception ex, string message, params object[] args);

		/// <summary>
		/// Write a critical log message
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Critical(string message, params object[] args);

		/// <summary>
		/// Write a critical log message
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="message">Log message</param>
		/// <param name="args">Message args</param>
		void Critical(Exception ex, string message, params object[] args);
	}
}

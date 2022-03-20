// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Serilog;
using Strasnote.Logging;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
{
	public class Debug_Tests
	{
		[Fact]
		public void Calls_Serilog_Debug_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Debug(message);
			logger.Debug(message, args);

			// Assert
			serilog.Received(1).Debug(SerilogLogger.Prefix + message, Array.Empty<object>());
			serilog.Received(1).Debug(SerilogLogger.Prefix + message, args);
		}
	}
}

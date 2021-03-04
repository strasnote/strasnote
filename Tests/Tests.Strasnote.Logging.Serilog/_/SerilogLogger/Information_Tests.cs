using System;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
{
	public class Information_Tests
	{
		[Fact]
		public void Calls_Serilog_Information_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Information(message);
			logger.Information(message, args);

			// Assert
			serilog.Received(1).Information(SerilogLogger.Prefix + message, Array.Empty<object>());
			serilog.Received(1).Information(SerilogLogger.Prefix + message, args);
		}
	}
}

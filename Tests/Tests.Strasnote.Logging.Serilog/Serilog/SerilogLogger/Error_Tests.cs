using System;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.Serilog_Tests
{
	public class Error_Tests
	{
		[Fact]
		public void Calls_Serilog_Error_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Error(message, args);

			// Assert
			serilog.Received().Error(SerilogLogger.Prefix + message, args);
		}

		[Fact]
		public void Calls_Serilog_Error_With_Exception_And_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var exception = new Exception(Rnd.Str);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Error(exception, message, args);

			// Assert
			serilog.Received().Error(exception, SerilogLogger.Prefix + message, args);
		}
	}
}

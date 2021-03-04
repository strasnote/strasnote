using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.Serilog_Tests
{
	public class Warning_Tests
	{
		[Fact]
		public void Calls_Serilog_Warning_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Warning(message, args);

			// Assert
			serilog.Received().Warning(SerilogLogger.Prefix + message, args);
		}
	}
}

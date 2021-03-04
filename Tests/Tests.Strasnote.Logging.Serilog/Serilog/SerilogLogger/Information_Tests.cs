using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.Serilog_Tests
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
			logger.Information(message, args);

			// Assert
			serilog.Received().Information(SerilogLogger.Prefix + message, args);
		}
	}
}

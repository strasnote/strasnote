using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;
using Strasnote.Logging;
using Xunit;

namespace Tests.Strasnote.Logging.ConsoleLog_Tests
{
	public class CreateConsoleLogger_Tests
	{
		[Fact]
		public void MinimumLevel_Is_Information()
		{
			// Arrange
			var logger = ConsoleLog.CreateConsoleLogger;

			// Act
			var r0 = logger.IsEnabled(LogEventLevel.Verbose);
			var r1 = logger.IsEnabled(LogEventLevel.Debug);
			var r2 = logger.IsEnabled(LogEventLevel.Information);
			var r3 = logger.IsEnabled(LogEventLevel.Warning);
			var r4 = logger.IsEnabled(LogEventLevel.Error);
			var r5 = logger.IsEnabled(LogEventLevel.Fatal);

			// Assert
			Assert.False(r0);
			Assert.False(r1);
			Assert.True(r2);
			Assert.True(r3);
			Assert.True(r4);
			Assert.True(r5);
		}
	}
}

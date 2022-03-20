// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Serilog;
using Serilog.Events;
using Strasnote.Logging;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
{
	public class IsEnabled_Tests
	{
		[Fact]
		public void Calls_Serilog_IsEnabled()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			const Microsoft.Extensions.Logging.LogLevel level = Microsoft.Extensions.Logging.LogLevel.Information;

			// Act
			logger.IsEnabled(level);

			// Assert
			serilog.Received().IsEnabled((LogEventLevel)level);
		}
	}
}

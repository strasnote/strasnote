// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
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
			logger.Warning(message);
			logger.Warning(message, args);

			// Assert
			serilog.Received(1).Warning(SerilogLogger.Prefix + message, Array.Empty<object>());
			serilog.Received(1).Warning(SerilogLogger.Prefix + message, args);
		}
	}
}

using System;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Parameterless_Uses_Default_Logger()
		{
			// Arrange
			var logger = Substitute.For<ILogger>();
			Log.Logger = logger;

			// Act
			new SerilogLogger().Information(Rnd.Str, Rnd.Int);

			// Assert
			logger.Received().Information(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

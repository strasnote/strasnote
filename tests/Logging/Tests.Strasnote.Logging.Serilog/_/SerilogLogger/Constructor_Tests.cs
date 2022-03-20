// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Serilog;
using Strasnote.Logging;

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

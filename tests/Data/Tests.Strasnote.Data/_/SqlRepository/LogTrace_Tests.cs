// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.SqlRepository_Tests
{
	public class LogTrace_Tests
	{
		[Fact]
		public void Calls_Log_Trace()
		{
			// Arrange
			var (repo, _, _, log, _) = SqlRepository_Setup.Get();
			var message = Rnd.Str;
			var args = new object[] { Rnd.Int, Rnd.Int };

			// Act
			repo.LogTraceTest(message, args);

			// Assert
			log.Received().Trace(message, args);
		}
	}
}

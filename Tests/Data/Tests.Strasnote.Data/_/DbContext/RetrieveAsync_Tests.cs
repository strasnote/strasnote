// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContext_Tests
{
	public class RetrieveAsync_Tests
	{
		[Fact]
		public void Logs_Operation()
		{
			// Arrange
			var (context, _, _, log, _) = DbContext_Setup.GetContext();

			// Act
			context.RetrieveAsync<long>(Rnd.Str, Rnd.Lng, System.Data.CommandType.Text);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}

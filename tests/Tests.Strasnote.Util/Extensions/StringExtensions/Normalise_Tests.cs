// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Util.StringExtensions_Tests
{
	public class Normalise_Tests
	{
		[Theory]
		[InlineData("one", "one")]
		[InlineData("ONE", "one")]
		[InlineData("  one  ", "one")]
		[InlineData("1  one 1 ", "1-one-1")]
		[InlineData("0123456789abcdefghijklmnopqrstuvwxyz", "0123456789abcdefghijklmnopqrstuvwxyz")]
		[InlineData("£% ^o&$(!(*$&N($)(*~@}{~¬!?><|)e)", "o-n-e")]
		public void Normalises_String(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.Normalise();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

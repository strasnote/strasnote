// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Hash_Tests
{
	public class Password_Tests
	{
		[Fact]
		public void Returns_32byte_Hashed_Password()
		{
			// Arrange
			var value = Rnd.Str;

			// Act
			var result = Hash.Password(value);

			// Assert
			Assert.Equal(32, result.Length);
		}

		[Fact]
		public void Returns_Same_Hash_Each_Time()
		{
			// Arrange
			var value = Rnd.Str;
			var hash = Hash.Password(value);

			// Act
			var result = Hash.Password(value);

			// Assert
			Assert.Equal(hash, result);
		}
	}
}

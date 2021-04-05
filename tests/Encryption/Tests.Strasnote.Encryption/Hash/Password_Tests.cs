// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Linq;
using Jeebs;
using Jeebs.Linq;
using Strasnote.Util;
using Xunit;
using static Strasnote.Encryption.Hash.Msg;

namespace Strasnote.Encryption.Hash_Tests
{
	public class Password_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_Password_Returns_None_With_NullPasswordExceptionMsg(string input)
		{
			// Arrange

			// Act
			var result = Hash.PasswordGeneric(input);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<GenericPasswordHashFailedException>(none);
		}

		[Fact]
		public void Returns_32byte_Hashed_Password()
		{
			// Arrange
			var value = Rnd.Str;

			// Act
			var result = Hash.PasswordGeneric(value);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(32, some.Length);
		}

		[Fact]
		public void Returns_Same_Hash_Each_Time()
		{
			// Arrange
			var value = Rnd.Str;

			// Act
			var result = from h0 in Hash.PasswordGeneric(value)
						 from h1 in Hash.PasswordGeneric(value)
						 select h0.SequenceEqual(h1);

			// Assert
			var some = result.AssertSome();
			Assert.True(some);
		}
	}
}

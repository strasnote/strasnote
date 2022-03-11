// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using MaybeF.Testing;
using Strasnote.Util;
using Xunit;
using static Strasnote.Encryption.Encrypt.R;

namespace Strasnote.Encryption.Encrypt_Tests
{
	public class String_Tests
	{
		[Fact]
		public void Invalid_Public_Key_Returns_None_With_UnableToEncryptStringExceptionMsg()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap() with { PublicKey = Array.Empty<byte>() };
			var value = Rnd.Str;

			// Act
			var result = Encrypt.String(value, encryptedKeyPair);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToEncryptStringExceptionReason>(none);
		}

		[Fact]
		public void Returns_Different_Contents_Each_Time()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = Rnd.Str;

			// Act
			var r0 = Encrypt.String(value, encryptedKeyPair);
			var r1 = Encrypt.String(value, encryptedKeyPair);

			// Assert
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0, s1);
		}

		public sealed record TestObject(string Foo);
	}
}

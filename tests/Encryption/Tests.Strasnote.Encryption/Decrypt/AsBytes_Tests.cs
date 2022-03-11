// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using MaybeF.Testing;
using Strasnote.Util;
using Xunit;
using static Strasnote.Encryption.Decrypt.R;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class AsBytes_Tests
	{
		[Fact]
		public void Empty_Encrypted_Contents_Returns_None_With_UnableToDecryptValueExceptionReason()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = Array.Empty<byte>();

			// Act
			var result = Decrypt.AsBytes(value, encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptValueExceptionReason>(none);
		}

		[Fact]
		public void Invalid_Encrypted_Contents_Returns_None_With_UnableToDecryptValueExceptionReason()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = Rnd.RndBytes.Get(64);

			// Act
			var result = Decrypt.AsBytes(value, encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptValueExceptionReason>(none);
		}

		[Fact]
		public void Invalid_Public_Key_Returns_None_With_UnableToDecryptValueExceptionReason()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var encrypted = Encrypt.String(Rnd.Str, encryptedKeyPair).UnsafeUnwrap();
			var invalidKeyPair = encryptedKeyPair with { PublicKey = Rnd.RndBytes.Get(64) };

			// Act
			var result = Decrypt.AsBytes(encrypted, invalidKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptValueExceptionReason>(none);
		}

		[Fact]
		public void Returns_Decrypted_Bytes()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = Rnd.Str;
			var valueAsBytes = Encoding.UTF8.GetBytes(value);
			var encrypted = Encrypt.String(value, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsBytes(encrypted, encryptedKeyPair, password);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(valueAsBytes, some);
		}
	}
}

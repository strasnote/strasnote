// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Jeebs;
using Sodium;
using Strasnote.Util;
using Xunit;
using static Strasnote.Encryption.Decrypt.Msg;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class PrivateKey_Tests
	{
		[Fact]
		public void Incorrect_Password_Returns_None_With_UnableToDecryptPrivateKeyExceptionMsg()
		{
			// Arrange
			var encryptedKeyPair = Keys.Generate(Rnd.Str).UnsafeUnwrap();

			// Act
			var result = Decrypt.PrivateKey(encryptedKeyPair, Rnd.Str);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptPrivateKeyExceptionMsg>(none);
		}

		[Fact]
		public void Invalid_Encrypted_Key_Returns_None_With_UnableToDecryptPrivateKeyExceptionMsg()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap() with { PrivateKey = Rnd.RndBytes.Get(32) };

			// Act
			var result = Decrypt.PrivateKey(encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptPrivateKeyExceptionMsg>(none);
		}

		[Fact]
		public void Incorrect_Nonce_Returns_None_With_UnableToDecryptPrivateKeyExceptionMsg()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap() with { Nonce = SecretBox.GenerateNonce() };

			// Act
			var result = Decrypt.PrivateKey(encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToDecryptPrivateKeyExceptionMsg>(none);
		}

		[Fact]
		public void Returns_Decrypted_Private_Key()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = PublicKeyBox.GenerateKeyPair();
			var encryptedKeyPair = Keys.WithEncryptedPrivateKey(keyPair, password).UnsafeUnwrap();

			// Act
			var result = Decrypt.PrivateKey(encryptedKeyPair, password);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(keyPair.PrivateKey, some);
		}
	}
}

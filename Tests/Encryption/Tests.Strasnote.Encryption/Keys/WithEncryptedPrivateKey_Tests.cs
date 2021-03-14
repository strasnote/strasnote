// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Sodium;
using Strasnote.Encryption;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Encryption.Keys_Tests
{
	public class WithEncryptedPrivateKey_Tests
	{
		[Fact]
		public void Generates_Encrypted_Private_Key()
		{
			// Arrange
			var keyPair = PublicKeyBox.GenerateKeyPair();
			var password = Rnd.Str;

			// Act
			var result = Keys.WithEncryptedPrivateKey(keyPair, password);

			// Assert
			Assert.Equal(keyPair.PublicKey, result.PublicKey);
			Assert.NotEqual(keyPair.PrivateKey, result.PrivateKey);
			Assert.NotEmpty(result.Nonce);
		}

		[Fact]
		public void Uses_Different_Nonce_Each_Time()
		{
			// Arrange
			var keyPair = PublicKeyBox.GenerateKeyPair();
			var password = Rnd.Str;

			// Act
			var r0 = Keys.WithEncryptedPrivateKey(keyPair, password);
			var r1 = Keys.WithEncryptedPrivateKey(keyPair, password);

			// Assert
			Assert.NotEqual(r0.PrivateKey, r1.PrivateKey);
			Assert.NotEqual(r0.Nonce, r1.Nonce);
		}
	}
}

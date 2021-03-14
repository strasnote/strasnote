// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class PrivateKey_Tests
	{
		[Fact]
		public void Returns_Decrypted_Private_Key()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Sodium.PublicKeyBox.GenerateKeyPair();
			var encryptedKeyPair = Keys.WithEncryptedPrivateKey(keyPair, password);

			// Act
			var result = Decrypt.PrivateKey(encryptedKeyPair, password);

			// Assert
			Assert.Equal(keyPair.PrivateKey, result);
		}
	}
}

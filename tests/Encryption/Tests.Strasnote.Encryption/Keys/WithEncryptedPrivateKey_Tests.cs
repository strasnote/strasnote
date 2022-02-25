// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Jeebs;
using Sodium;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Keys_Tests
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
			var some = result.AssertSome();
			Assert.Equal(some.PublicKey, some.PublicKey);
			Assert.NotEqual(keyPair.PrivateKey, some.PrivateKey);
			Assert.NotEmpty(some.Nonce);
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
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0.PrivateKey, s1.PrivateKey);
			Assert.NotEqual(s0.Nonce, s1.Nonce);
		}
	}
}

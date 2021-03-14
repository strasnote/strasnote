// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Encrypt_Tests
{
	public class PrivateKey_Tests
	{
		[Fact]
		public void Encrypts_Using_Password()
		{
			// Arrange
			var key = Rnd.RndBytes.Get(32);
			var password = Rnd.Str;

			// Act
			var value = Encrypt.PrivateKey(key, password);

			// Assert
			Assert.NotEqual(key, value.key);
		}

		[Fact]
		public void Uses_Different_Nonce_Each_Time()
		{
			// Arrange
			var key = Rnd.RndBytes.Get(32);
			var password = Rnd.Str;

			// Act
			var r0 = Encrypt.PrivateKey(key, password);
			var r1 = Encrypt.PrivateKey(key, password);

			// Assert
			Assert.NotEqual(r0.nonce, r1.nonce);
		}

		[Fact]
		public void Can_Be_Decrypted()
		{
			// Arrange
			var key = Rnd.RndBytes.Get(32);
			var password = Rnd.Str;
			var encrypted = Encrypt.PrivateKey(key, password);

			// Act
			var result = Decrypt.PrivateKey(new(Rnd.RndBytes.Get(32), encrypted.key, encrypted.nonce), password);

			// Assert
			Assert.Equal(key, result);
		}
	}
}

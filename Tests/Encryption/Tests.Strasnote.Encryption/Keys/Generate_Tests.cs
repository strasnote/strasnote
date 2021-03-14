// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Sodium;
using Strasnote.Encryption;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Encryption.Keys_Tests
{
	public class Generate_Tests
	{
		[Fact]
		public void Generates_New_EncryptedKeyPair()
		{
			// Arrange
			var password = Rnd.Str;

			// Act
			var result = Keys.Generate(password);

			// Assert
			Assert.NotEmpty(result.PublicKey);
			Assert.NotEmpty(result.PrivateKey);
			Assert.NotEmpty(result.Nonce);
		}
	}
}

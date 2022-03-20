// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using MaybeF.Testing;

namespace Strasnote.Encryption.Keys_Tests
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
			var some = result.AssertSome();
			Assert.NotEmpty(some.PublicKey);
			Assert.NotEmpty(some.PrivateKey);
			Assert.NotEmpty(some.Nonce);
		}

		[Fact]
		public void Generates_New_KeyPair_Each_Time()
		{
			// Arrange
			var password = Rnd.Str;

			// Act
			var r0 = Keys.Generate(password);
			var r1 = Keys.Generate(password);

			// Assert
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0.PublicKey, s1.PublicKey);
			Assert.NotEqual(s0.PrivateKey, s1.PrivateKey);
			Assert.NotEqual(s0.Nonce, s1.Nonce);
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class AsObject_Tests
	{
		[Fact]
		public void Returns_Decrypted_Object()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password);
			var foo = Rnd.Str;
			var value = new TestObject(foo);
			var encrypted = Encrypt.Object(value, encryptedKeyPair);

			// Act
			var result = Decrypt.AsObject<TestObject>(encrypted, encryptedKeyPair, password);

			// Assert
			Assert.Equal(value, result);
		}

		public sealed record TestObject(string Foo);
	}
}

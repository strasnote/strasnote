// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class AsString_Tests
	{
		[Fact]
		public void Returns_Decrypted_String()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password);
			var value = Rnd.Str;
			var encrypted = Encrypt.String(value, encryptedKeyPair);

			// Act
			var result = Decrypt.AsString(encrypted, encryptedKeyPair, password);

			// Assert
			Assert.Equal(value, result);
		}
	}
}

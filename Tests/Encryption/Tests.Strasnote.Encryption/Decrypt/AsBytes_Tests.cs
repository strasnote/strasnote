// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class AsBytes_Tests
	{
		[Fact]
		public void Returns_Decrypted_Bytes()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password);
			var value = Rnd.Str;
			var valueAsBytes = Encoding.UTF8.GetBytes(value);
			var encrypted = Encrypt.String(value, encryptedKeyPair);

			// Act
			var result = Decrypt.AsBytes(encrypted, encryptedKeyPair, password);

			// Assert
			Assert.Equal(valueAsBytes, result);
		}
	}
}

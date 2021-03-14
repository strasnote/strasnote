// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text.Json;
using Sodium;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Encrypt_Tests
{
	public class String_Tests
	{
		[Fact]
		public void Returns_Different_Contents_Each_Time()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Keys.Generate(password);
			var foo = Rnd.Str;
			var value = new TestObject(foo);
			var json = JsonSerializer.Serialize(value);

			// Act
			var r0 = Encrypt.String(json, keyPair);
			var r1 = Encrypt.String(json, keyPair);

			// Assert
			Assert.NotEqual(r0, r1);
		}

		[Fact]
		public void Can_Be_Decrypted()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Keys.Generate(password);
			var foo = Rnd.Str;
			var value = new TestObject(foo);
			var json = JsonSerializer.Serialize(value);
			var encrypted = Encrypt.String(json, keyPair);

			// Act
			var result = Decrypt.AsString(encrypted, keyPair, password);

			// Assert
			Assert.Equal(json, result);
		}

		public sealed record TestObject(string Foo);
	}
}

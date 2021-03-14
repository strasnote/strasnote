// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text.Json;
using Sodium;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Encrypt_Tests
{
	public class Object_Tests
	{
		[Fact]
		public void Returns_Different_Contents_Each_Time()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Keys.Generate(password);
			var foo = Rnd.Str;
			var value = new TestObject(foo);

			// Act
			var r0 = Encrypt.Object(value, keyPair);
			var r1 = Encrypt.Object(value, keyPair);

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
			var encrypted = Encrypt.Object(value, keyPair);

			// Act
			var result = Decrypt.AsString(encrypted, keyPair, password);

			// Assert
			Assert.Equal(json, result);
		}

		public sealed record TestObject(string Foo);
	}
}

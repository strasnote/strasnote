// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Jeebs;
using Strasnote.Util;
using Xunit;
using static Strasnote.Encryption.Decrypt.Msg;

namespace Strasnote.Encryption.Decrypt_Tests
{
	public class AsObject_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_Json_Returns_None_With_JsonDeserialiseExceptionMsg(TestObject input)
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var encryptedObject = Encrypt.Object(input, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsObject<TestObject>(encryptedObject, encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<JsonDeserialiseExceptionMsg>(none);
		}

		[Fact]
		public void Empty_Json_Returns_None_With_JsonDeserialiseExceptionMsg()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var encryptedObject = Encrypt.String(string.Empty, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsObject<TestObject>(encryptedObject, encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<JsonDeserialiseExceptionMsg>(none);
		}

		[Fact]
		public void Incorrect_Type_With_Matching_Parameters_Returns_None_With_JsonDeserialiseExceptionMsg()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = new TestObject(Rnd.Str);
			var encryptedObject = Encrypt.Object(value, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsObject<IncorrectTypeWithMatchingProperty>(encryptedObject, encryptedKeyPair, password);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<JsonDeserialiseExceptionMsg>(none);
		}

		[Fact]
		public void Incorrect_Type_Returns_Some_With_Empty_Properties()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = new TestObject(Rnd.Str);
			var encryptedObject = Encrypt.Object(value, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsObject<IncorrectType>(encryptedObject, encryptedKeyPair, password);

			// Assert
			var some = result.AssertSome();
			Assert.Null(some.Bar);
		}

		[Fact]
		public void Correct_Type_Returns_Decrypted_Object()
		{
			// Arrange
			var password = Rnd.Str;
			var encryptedKeyPair = Keys.Generate(password).UnsafeUnwrap();
			var value = new TestObject(Rnd.Str);
			var encryptedObject = Encrypt.Object(value, encryptedKeyPair).UnsafeUnwrap();

			// Act
			var result = Decrypt.AsObject<TestObject>(encryptedObject, encryptedKeyPair, password);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		public sealed record TestObject(string Foo);

		public sealed record IncorrectType(TestObject Bar);

		public sealed record IncorrectTypeWithMatchingProperty(TestObject Foo);
	}
}

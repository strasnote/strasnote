// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using MaybeF.Testing;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Encryption.Encrypt_Tests
{
	public class Object_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_Object_Returns_Array(TestObject input)
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Keys.Generate(password).UnsafeUnwrap();

			// Act
			var result = Encrypt.Object(input, keyPair);

			// Assert
			var some = result.AssertSome();
			Assert.NotEmpty(some);
		}

		[Fact]
		public void Returns_Different_Contents_Each_Time()
		{
			// Arrange
			var password = Rnd.Str;
			var keyPair = Keys.Generate(password).UnsafeUnwrap();
			var foo = Rnd.Str;
			var value = new TestObject(foo);

			// Act
			var r0 = Encrypt.Object(value, keyPair);
			var r1 = Encrypt.Object(value, keyPair);

			// Assert
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0, s1);
		}

		public sealed record TestObject(string Foo);
	}
}

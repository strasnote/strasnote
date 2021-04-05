// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Jeebs;
using Jeebs.Linq;
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
			var result = from encrypted in Encrypt.PrivateKey(key, password)
						 select encrypted.key;

			// Assert
			var some = result.AssertSome();
			Assert.NotEqual(key, some);
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
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0.nonce, s1.nonce);
		}
	}
}

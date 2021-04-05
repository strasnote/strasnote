// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Jeebs;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Migrate;
using Strasnote.Util;
using Xunit;
using static Strasnote.Data.Migrate.DefaultUser.Msg;

namespace Strasnote.Data.DefaultUser_Tests
{
	public class GetEmailAndPassword_Tests
	{
		[Fact]
		public void Email_Null_Returns_None_With_EmailAndPasswordMustBothBeSetMsg()
		{
			// Arrange
			var config = new UserConfig { Email = null, Password = Rnd.Str };

			// Act
			var result = DefaultUser.GetEmailAndPassword(config);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<EmailAndPasswordMustBothBeSetMsg>(none);
		}

		[Fact]
		public void Password_Null_Returns_None_With_EmailAndPasswordMustBothBeSetMsg()
		{
			// Arrange
			var config = new UserConfig { Email = Rnd.Str, Password = null };

			// Act
			var result = DefaultUser.GetEmailAndPassword(config);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<EmailAndPasswordMustBothBeSetMsg>(none);
		}

		[Fact]
		public void Returns_Email_And_Hashed_Password()
		{
			// Arrange
			var email = Rnd.Str;
			var password = Rnd.Str;
			var config = new UserConfig { Email = email, Password = password };
			var hasher = new PasswordHasher<UserEntity>();

			// Act
			var result = DefaultUser.GetEmailAndPassword(config);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(email, some.email);
			Assert.Equal(PasswordVerificationResult.Success, hasher.VerifyHashedPassword(new(), some.password, password));
		}
	}
}

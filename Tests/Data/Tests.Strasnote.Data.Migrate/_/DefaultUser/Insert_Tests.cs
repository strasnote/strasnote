// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Migrate;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DefaultUser_Tests
{
	public class Insert_Tests
	{
		[Fact]
		public void Logs_Error_To_Error_Log()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var user = Substitute.For<IUserRepository>();
			var config = new UserConfig();

			// Act
			DefaultUser.Insert(log, user, config);

			// Assert
			log.Received().Error(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public async Task Calls_Repo_CreateAsync_With_New_User_Entity()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var user = Substitute.For<IUserRepository>();
			var config = new UserConfig { Email = Rnd.Str, Password = Rnd.Str };

			// Act
			DefaultUser.Insert(log, user, config);

			// Assert
			await user.Received().CreateAsync(Arg.Any<UserEntity>());
		}
	}
}

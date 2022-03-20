// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Data.Migrate.DefaultUser_Tests
{
	public class InsertAsync_Tests
	{
		[Fact]
		public async Task Logs_Error_To_Error_Log()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var user = Substitute.For<IUserRepository>();
			var config = new UserConfig();

			// Act
			await DefaultUser.InsertAsync(log, user, config);

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
			await DefaultUser.InsertAsync(log, user, config);

			// Assert
			await user.Received().CreateAsync(Arg.Any<UserEntity>());
		}
	}
}

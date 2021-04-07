// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Exceptions;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_FindByNameAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();
		public Tests_FindByNameAsync()
		{
			userRepository.RetrieveByUsernameAsync<UserEntity>(Arg.Any<string>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			var result = await userStore.FindByNameAsync(Rnd.Str, new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task RetrieveByUsernameAsync_CreateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			await userStore.FindByNameAsync(Rnd.Str, new CancellationToken());

			// Assert
			await userRepository.Received(1).RetrieveByUsernameAsync<UserEntity>(Arg.Any<string>());
		}

		[Fact]
		public async Task Throws_Exception_When_User_Not_Found()
		{
			// Arrange
			var userStore = new UserStore(userRepository);
			userRepository.RetrieveByUsernameAsync<UserEntity>(Arg.Any<string>()).Returns(Task.FromResult<UserEntity?>(null));

			// Act
			Task action() => userStore.FindByNameAsync(Rnd.Str, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<UserNotFoundByUsernameException>(action);
		}
	}
}

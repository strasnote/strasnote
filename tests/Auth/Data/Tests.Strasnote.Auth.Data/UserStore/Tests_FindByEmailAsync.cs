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
	public sealed class Tests_FindByEmailAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		public Tests_FindByEmailAsync()
		{
			userRepository.RetrieveByEmailAsync<UserEntity>(Arg.Any<string>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			var result = await userStore.FindByEmailAsync(Rnd.Str, new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task UserRepository_RetrieveByEmailAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			await userStore.FindByEmailAsync(Rnd.Str, new CancellationToken()).ConfigureAwait(false);

			// Assert
			await userRepository.Received(1).RetrieveByEmailAsync<UserEntity>(Arg.Any<string>()).ConfigureAwait(false);
		}

		[Fact]
		public async Task Throws_Exception_When_User_Not_Found()
		{
			// Arrange
			var userStore = new UserStore(userRepository);
			userRepository.RetrieveByEmailAsync<UserEntity>(Arg.Any<string>()).Returns(Task.FromResult<UserEntity?>(null));

			// Act
			Task action() => userStore.FindByEmailAsync(Rnd.Str, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<UserNotFoundByEmailException>(action).ConfigureAwait(false);
		}
	}
}

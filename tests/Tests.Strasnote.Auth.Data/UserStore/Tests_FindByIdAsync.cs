// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Exceptions;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_FindByIdAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		public Tests_FindByIdAsync()
		{
			userRepository.RetrieveAsync<UserEntity>(Arg.Any<ulong>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			var result = await userStore.FindByIdAsync("1", new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task UserRepository_RetrieveByIdAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			await userStore.FindByIdAsync("1", new CancellationToken());

			// Assert
			await userRepository.Received(1).RetrieveAsync<UserEntity>(Arg.Any<ulong>());
		}

		[Fact]
		public async Task Throws_Exception_When_Id_Is_Invalid_Long()
		{
			// Arrange
			var userStore = new UserStore(userRepository);
			userRepository.RetrieveAsync<UserEntity?>(Arg.Any<ulong>()).Returns(Task.FromResult<UserEntity?>(null));

			// Act
			Task action() => userStore.FindByIdAsync(Rnd.Str, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<UserNotFoundInvalidIdException>(action);
		}

		[Fact]
		public async Task Throws_Exception_When_User_Not_Found()
		{
			// Arrange
			var userStore = new UserStore(userRepository);
			userRepository.RetrieveAsync<UserEntity?>(Arg.Any<ulong>()).Returns(Task.FromResult<UserEntity?>(null));

			// Act
			Task action() => userStore.FindByIdAsync("1", new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<UserNotFoundByIdException>(action);
		}
	}
}

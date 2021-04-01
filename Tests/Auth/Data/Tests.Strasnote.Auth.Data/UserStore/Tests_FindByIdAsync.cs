// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_FindByIdAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		public Tests_FindByIdAsync()
		{
			userRepository.RetrieveAsync<UserEntity>(Arg.Any<long>())
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
			await userRepository.Received(1).RetrieveAsync<UserEntity>(Arg.Any<long>());
		}
	}
}

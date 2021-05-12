// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_CreateAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		public Tests_CreateAsync()
		{
			userRepository.CreateAsync(Arg.Any<UserEntity>())
				.Returns(Rnd.Lng);
		}

		[Fact]
		public async Task IdentityResult_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			var result = await userStore.CreateAsync(new UserEntity(), new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<IdentityResult>(result);
		}

		[Fact]
		public async Task UserRepository_CreateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			await userStore.CreateAsync(new UserEntity(), new CancellationToken()).ConfigureAwait(false);

			// Assert
			await userRepository.Received(1).CreateAsync(Arg.Any<UserEntity>()).ConfigureAwait(false);
		}
	}
}

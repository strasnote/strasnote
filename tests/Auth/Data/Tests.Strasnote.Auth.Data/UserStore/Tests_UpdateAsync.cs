// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_UpdateAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task UserRepository_UpdateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			// Act
			await userStore.UpdateAsync(userEntity, new CancellationToken());

			// Assert
			await userRepository.Received(1).UpdateAsync(0, userEntity);
		}
	}
}

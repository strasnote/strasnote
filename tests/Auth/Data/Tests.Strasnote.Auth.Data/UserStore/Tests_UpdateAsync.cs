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
			await userRepository.Received(1).UpdateAsync<UserEntity>(userEntity);
		}
	}
}

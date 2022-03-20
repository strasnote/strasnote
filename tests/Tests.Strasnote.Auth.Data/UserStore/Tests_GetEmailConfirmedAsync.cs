// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_GetEmailConfirmedAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task EmailConfirmed_bool_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity
			{
				EmailConfirmed = true
			};

			// Act
			var result = await userStore.GetEmailConfirmedAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.EmailConfirmed, result);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			Task action() => userStore.GetEmailConfirmedAsync(null!, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(action);
		}
	}
}

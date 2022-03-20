// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_SetEmailAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task Email_On_UserEntity_Is_Set_To_Email_Arg()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			string email = Rnd.Str;

			// Act
			await userStore.SetEmailAsync(userEntity, email, new CancellationToken());

			// Assert
			Assert.Equal(email, userEntity.Email);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act
			Task action() => userStore.SetEmailAsync(null!, Rnd.Str, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(action);
		}
	}
}

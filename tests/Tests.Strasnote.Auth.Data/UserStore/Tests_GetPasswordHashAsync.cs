// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_GetPasswordHashAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task PasswordHash_String_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity
			{
				PasswordHash = Rnd.Str
			};

			// Act
			var result = await userStore.GetPasswordHashAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.PasswordHash, result);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() => userStore.GetPasswordHashAsync(null!, new CancellationToken()));
		}
	}
}

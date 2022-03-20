// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_SetNormalizedUserNameAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task NormalizedUserName_On_UserEntity_Is_Set_To_NormalizedName_Arg()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			string normalizedUsername = Rnd.Str;

			// Act
			await userStore.SetNormalizedUserNameAsync(userEntity, normalizedUsername, new CancellationToken());

			// Assert
			Assert.Equal(normalizedUsername, userEntity.NormalizedUserName);
		}
	}
}

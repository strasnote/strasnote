// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_SetNormalizedEmailAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task NormalizedEmail_On_UserEntity_Is_Set_To_NormalizedEmail_Arg()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			string normalizedEmail = Rnd.Str;

			// Act
			await userStore.SetNormalizedEmailAsync(userEntity, normalizedEmail, new CancellationToken());

			// Assert
			Assert.Equal(normalizedEmail, userEntity.NormalizedEmail);
		}
	}
}

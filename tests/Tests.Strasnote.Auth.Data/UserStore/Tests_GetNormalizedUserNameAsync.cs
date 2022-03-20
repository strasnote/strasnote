// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_GetNormalizedUserNameAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task Normalized_UserName_String_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity
			{
				NormalizedUserName = Rnd.Str
			};

			// Act
			var result = await userStore.GetNormalizedUserNameAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.NormalizedUserName, result);
		}
	}
}

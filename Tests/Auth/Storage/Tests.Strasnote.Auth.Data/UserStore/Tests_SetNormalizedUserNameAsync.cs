// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_SetNormalizedUserNameAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task NormalizedUserName_On_UserEntity_Is_Set_To_NormalizedName_Arg()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity();

			string normalizedUsername = Rnd.Str;

			// Act
			await userStore.SetNormalizedUserNameAsync(userEntity, normalizedUsername, new CancellationToken());

			// Assert
			Assert.Equal(normalizedUsername, userEntity.NormalizedUserName);
		}
	}
}

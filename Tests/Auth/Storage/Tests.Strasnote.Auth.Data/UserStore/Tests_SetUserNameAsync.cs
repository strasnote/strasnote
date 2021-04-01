// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
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
	public sealed class Tests_SetUserNameAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task UserName_On_UserEntity_Is_Set_To_UserName_Arg()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity();

			string username = Rnd.Str;

			// Act
			await userStore.SetUserNameAsync(userEntity, username, new CancellationToken());

			// Assert
			Assert.Equal(username, userEntity.UserName);
		}
	}
}

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
	public sealed class Tests_SetPasswordHashAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task PasswordHash_On_UserEntity_Is_Set_To_PasswordHash_Arg()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity();

			string passwordHash = Rnd.Str;

			// Act
			await userStore.SetPasswordHashAsync(userEntity, passwordHash, new CancellationToken());

			// Assert
			Assert.Equal(passwordHash, userEntity.PasswordHash);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act
			Task action() => userStore.SetPasswordHashAsync(null!, Rnd.Str, new CancellationToken());

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(action);
		}
	}
}

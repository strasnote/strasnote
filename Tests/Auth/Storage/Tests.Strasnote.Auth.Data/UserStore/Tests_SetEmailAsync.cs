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
	public sealed class Tests_SetEmailAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task Email_On_UserEntity_Is_Set_To_Email_Arg()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

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
			var userStore = new UserStore(userContext, roleContext);

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() =>
				userStore.SetEmailAsync(Arg.Any<UserEntity>(), Arg.Any<string>(), new CancellationToken()));
		}
	}
}

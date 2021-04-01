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
	public sealed class Tests_GetEmailAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task Email_String_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity
			{
				Email = Rnd.Str
			};

			// Act
			var result = await userStore.GetEmailAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.Email, result);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() => userStore.GetEmailAsync(null!, new CancellationToken()));
		}
	}
}

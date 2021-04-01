// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_GetEmailConfirmedAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task EmailConfirmed_bool_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity
			{
				EmailConfirmed = true
			};

			// Act
			var result = await userStore.GetEmailConfirmedAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.EmailConfirmed, result);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() => userStore.GetEmailConfirmedAsync(null!, new CancellationToken()));
		}
	}
}

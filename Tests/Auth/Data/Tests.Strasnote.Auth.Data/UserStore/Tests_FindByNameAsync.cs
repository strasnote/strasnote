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
	public sealed class Tests_FindByNameAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		public Tests_FindByNameAsync()
		{
			userContext.RetrieveByUsernameAsync<UserEntity>(Arg.Any<string>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act
			var result = await userStore.FindByNameAsync(Rnd.Str, new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task RetrieveByUsernameAsync_CreateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act
			await userStore.FindByNameAsync(Rnd.Str, new CancellationToken());

			// Assert
			await userContext.Received(1).RetrieveByUsernameAsync<UserEntity>(Arg.Any<string>());
		}
	}
}

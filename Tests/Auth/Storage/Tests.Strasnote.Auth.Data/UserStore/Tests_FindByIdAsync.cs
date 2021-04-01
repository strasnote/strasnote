// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_FindByIdAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		public Tests_FindByIdAsync()
		{
			userContext.RetrieveByIdAsync<UserEntity>(Arg.Any<long>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act
			var result = await userStore.FindByIdAsync("1", new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task UserContext_RetrieveByIdAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			// Act
			await userStore.FindByIdAsync("1", new CancellationToken());

			// Assert
			await userContext.Received(1).RetrieveByIdAsync<UserEntity>(Arg.Any<long>());
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
    public sealed class Tests_CreateAsync
    {
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		private readonly IdentityResult identityResult = IdentityResult.Success;

		public Tests_CreateAsync()
		{
			userContext.CreateAsync<IdentityResult>(Arg.Any<UserEntity>())
				.Returns(identityResult);
		}

		[Fact]
		public async Task IdentityResult_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act
			var result = await userStore.CreateAsync(new UserEntity(), new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<IdentityResult>(result);
		}

		[Fact]
		public async Task UserContext_CreateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act
			var result = await userStore.CreateAsync(new UserEntity(), new CancellationToken());

			// Assert
			await userContext.Received(1).CreateAsync<IdentityResult>(Arg.Any<UserEntity>());
		}
    }
}

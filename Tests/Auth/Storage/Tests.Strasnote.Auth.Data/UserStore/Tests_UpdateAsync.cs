// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
	public sealed class Tests_UpdateAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task UserContext_UpdateAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity();

			// Act
			await userStore.UpdateAsync(userEntity, new CancellationToken());

			// Assert
			await userContext.Received(1).UpdateAsync<IdentityResult>(userEntity);
		}
	}
}

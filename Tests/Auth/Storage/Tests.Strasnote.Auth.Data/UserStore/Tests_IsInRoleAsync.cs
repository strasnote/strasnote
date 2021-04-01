// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_IsInRoleAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task Returns_True_If_User_Is_In_Role()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity();

			List<RoleEntity> roles = new()
			{
				new RoleEntity
				{
					Name = Rnd.Str
				}
			};

			roleContext.RetrieveForUserAsync<RoleEntity>(Arg.Any<long>())
					.Returns(roles);

			// Act
			var result = await userStore.IsInRoleAsync(userEntity, roles[0].Name, new CancellationToken());

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task Returns_False_If_User_Is_Not_In_Role()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity();

			List<RoleEntity> roles = new()
			{
				new RoleEntity
				{
					Name = Rnd.Str
				}
			};

			roleContext.RetrieveForUserAsync<RoleEntity>(Arg.Any<long>())
					.Returns(roles);

			// Act
			var result = await userStore.IsInRoleAsync(userEntity, Rnd.Str, new CancellationToken());

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task ArgumentNullException_Thrown_When_UserEntity_Null()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() => userStore.GetRolesAsync(null!, new CancellationToken()));
		}
	}
}

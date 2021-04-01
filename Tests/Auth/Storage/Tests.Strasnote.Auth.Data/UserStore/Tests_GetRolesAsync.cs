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
	public sealed class Tests_GetRolesAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task List_Of_Role_Names_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity();

			List<RoleEntity> roles = new()
			{
				new RoleEntity
				{
					Name = Rnd.Str
				},
				new RoleEntity
				{
					Name = Rnd.Str
				}
			};

			roleContext.RetrieveForUserAsync<RoleEntity>(Arg.Any<long>())
					.Returns(roles);

			var expectedRoles = roles.Select(x => x.Name);

			// Act
			var result = await userStore.GetRolesAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(expectedRoles, result);
		}

		[Fact]
		public async Task Empty_List_Of_Strings_Returned_If_User_Doesnt_Have_Any_Roles()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity();

			List<RoleEntity> roles = new();

			roleContext.RetrieveForUserAsync<RoleEntity>(Arg.Any<long>())
					.ReturnsNull();

			var expectedRoles = roles.Select(x => x.Name);

			// Act
			var result = await userStore.GetRolesAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(expectedRoles, result);
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

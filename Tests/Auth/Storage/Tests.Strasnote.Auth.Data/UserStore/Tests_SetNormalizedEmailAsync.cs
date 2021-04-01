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
	public sealed class Tests_SetNormalizedEmailAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task NormalizedEmail_On_UserEntity_Is_Set_To_NormalizedEmail_Arg()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity();

			string normalizedEmail = Rnd.Str;

			// Act
			await userStore.SetNormalizedEmailAsync(userEntity, normalizedEmail, new CancellationToken());

			// Assert
			Assert.Equal(normalizedEmail, userEntity.NormalizedEmail);
		}
	}
}

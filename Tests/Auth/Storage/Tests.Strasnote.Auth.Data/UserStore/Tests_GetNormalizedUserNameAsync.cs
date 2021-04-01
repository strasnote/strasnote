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
	public sealed class Tests_GetNormalizedUserNameAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task User_Id_String_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity
			{
				NormalizedUserName = Rnd.Str
			};

			// Act
			var result = await userStore.GetNormalizedUserNameAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.NormalizedUserName, result);
		}
	}
}

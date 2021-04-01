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
	public sealed class Tests_GetUserNameAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();

		[Fact]
		public async Task UserName_String_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext);

			var userEntity = new UserEntity
			{
				UserName = Rnd.Str
			};

			// Act
			var result = await userStore.GetUserNameAsync(userEntity, new CancellationToken());

			// Assert
			Assert.Equal(userEntity.UserName, result);
		}
	}
}

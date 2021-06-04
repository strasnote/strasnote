// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Security.Principal;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.AppBase.AppContext_Tests
{
	public class GetUserIsAuthenticated_Tests
	{
		[Fact]
		public void UserId_Null_Returns_False()
		{
			// Arrange

			// Act
			var result = WebAppContext.GetUserIsAuthenticated(null, Substitute.For<IIdentity>());

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void IIdentity_Null_Returns_False()
		{
			// Arrange
			var id = Rnd.Lng;

			// Act
			var result = WebAppContext.GetUserIsAuthenticated(id, null);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void UserId_Not_Null_And_IIdentity_IsAuthenticated_False_Returns_False()
		{
			// Arrange
			var id = Rnd.Lng;
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(false);

			// Act
			var result = WebAppContext.GetUserIsAuthenticated(id, identity);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void UserId_Not_Null_And_IIdentity_IsAuthenticated_True_Returns_True()
		{
			// Arrange
			var id = Rnd.Lng;
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(true);

			// Act
			var result = WebAppContext.GetUserIsAuthenticated(id, identity);

			// Assert
			Assert.True(result);
		}
	}
}

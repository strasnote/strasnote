// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.AppBase.Abstracts;
using Xunit;

namespace Strasnote.Util.Is_Tests
{
	public class AuthenticatedUser_Tests
	{
		[Fact]
		public void IsAuthenticated_False_Runs_Otherwise()
		{
			// Arrange
			var ctx = Substitute.For<IAppContext>();
			ctx.IsAuthenticated.Returns(false);

			var then = Substitute.For<Func<long, Task<string>>>();
			var otherwise = Substitute.For<Func<string>>();

			// Act
			var result = Is.AuthenticatedUser(ctx, then, otherwise);

			// Assert
			then.DidNotReceiveWithAnyArgs().Invoke(default);
			otherwise.ReceivedWithAnyArgs().Invoke();
		}

		[Fact]
		public void CurrentUserId_Null_Runs_Otherwise()
		{
			// Arrange
			var ctx = Substitute.For<IAppContext>();
			ctx.IsAuthenticated.Returns(true);

			var then = Substitute.For<Func<long, Task<string>>>();
			var otherwise = Substitute.For<Func<string>>();

			// Act
			var result = Is.AuthenticatedUser(ctx, then, otherwise);

			// Assert
			then.DidNotReceiveWithAnyArgs().Invoke(default);
			otherwise.ReceivedWithAnyArgs().Invoke();
		}

		[Fact]
		public void Valid_User_Runs_Then()
		{
			// Arrange
			var ctx = Substitute.For<IAppContext>();
			var userId = Rnd.Lng;
			ctx.IsAuthenticated.Returns(true);
			ctx.CurrentUserId.Returns(userId);

			var then = Substitute.For<Func<long, Task<string>>>();
			var otherwise = Substitute.For<Func<string>>();

			// Act
			var result = Is.AuthenticatedUser(ctx, then, otherwise);

			// Assert
			then.Received().Invoke(userId);
			otherwise.DidNotReceiveWithAnyArgs().Invoke();
		}
	}
}

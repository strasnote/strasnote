// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.AppBase.AppContext_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties_For_Authenticated_User()
		{
			// Arrange
			var id = Rnd.Lng;
			var idClaim = new Claim(ClaimTypes.NameIdentifier, id.ToString());
			var claims = new List<Claim> { idClaim };

			var authenticatedIdentity = Substitute.For<IIdentity>();
			authenticatedIdentity.IsAuthenticated.Returns(true);

			var identity = new ClaimsIdentity(authenticatedIdentity, claims, Rnd.Str, null, null);

			var principal = new ClaimsPrincipal(identity);

			var context = Substitute.For<HttpContext>();
			context.User.Returns(principal);

			var accessor = Substitute.For<IHttpContextAccessor>();
			accessor.HttpContext.Returns(context);

			// Act
			var result = new WebAppContext(accessor);

			// Assert
			Assert.Equal(id, result.CurrentUserId);
			Assert.True(result.IsAuthenticated);
		}
	}
}

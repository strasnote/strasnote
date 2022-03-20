// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Security.Claims;

namespace Strasnote.AppBase.AppContext_Tests
{
	public class GetCurrentUserId_Tests
	{
		[Fact]
		public void NameIdentifer_Claim_Not_Present_Returns_Null()
		{
			// Arrange
			var claims = new List<Claim>();

			// Act
			var result = WebAppContext.GetCurrentUserId(claims);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void NameIdentifier_Claim_Invalid_Id_Returns_Null()
		{
			// Arrange
			var id = Rnd.Str;
			var idClaim = new Claim(ClaimTypes.NameIdentifier, id);
			var claims = new List<Claim> { idClaim };

			// Act
			var result = WebAppContext.GetCurrentUserId(claims);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void Returns_Id_Value()
		{
			// Arrange
			var id = Rnd.Ulng;
			var idClaim = new Claim(ClaimTypes.NameIdentifier, id.ToString());
			var claims = new List<Claim> { idClaim };

			// Act
			var result = WebAppContext.GetCurrentUserId(claims);

			// Assert
			Assert.Equal(id, result);
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class RefreshToken_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "auth.refresh_token";

			// Act
			var result = tables.RefreshToken;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

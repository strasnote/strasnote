// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlDbTables_Tests
{
	public class RefreshToken_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new MySqlDbTables();
			const string expected = "auth.refresh_token";

			// Act
			var result = tables.RefreshToken;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class User_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "auth.user";

			// Act
			var result = tables.User;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

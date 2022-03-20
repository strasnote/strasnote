// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class Tag_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "main.tag";

			// Act
			var result = tables.Tag;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

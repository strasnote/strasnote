// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class Folder_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "main.folder";

			// Act
			var result = tables.Folder;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

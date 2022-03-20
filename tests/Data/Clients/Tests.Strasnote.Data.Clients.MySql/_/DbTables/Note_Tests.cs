// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class Note_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "main.note";

			// Act
			var result = tables.Note;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

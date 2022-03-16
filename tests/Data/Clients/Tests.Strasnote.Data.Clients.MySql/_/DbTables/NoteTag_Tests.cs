// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.Clients.MySql.DbTables_Tests
{
	public class NoteTag_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new DbTables();
			const string expected = "main.note_tag";

			// Act
			var result = tables.NoteTag;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

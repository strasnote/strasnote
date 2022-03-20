// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.StoredProcedure_Tests
{
	public class AddTagToNote_Tests
	{
		[Fact]
		public void Returns_Correct_Procedure_Name()
		{
			// Arrange
			const string expected = "AddTagToNote";

			// Act
			var result = StoredProcedure.AddTagToNote;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

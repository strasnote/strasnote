// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.StoredProcedure_Tests
{
	public class RemoveTagFromNotes_Tests
	{
		[Fact]
		public void Returns_Correct_Procedure_Name()
		{
			// Arrange
			const string expected = "RemoveTagFromNotes";

			// Act
			var result = StoredProcedure.RemoveTagFromNotes;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

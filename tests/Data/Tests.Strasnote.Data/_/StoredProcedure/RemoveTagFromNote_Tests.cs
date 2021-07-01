// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.StoredProcedure_Tests
{
	public class RemoveTagFromNote_Tests
	{
		[Fact]
		public void Returns_Correct_Procedure_Name()
		{
			// Arrange
			const string expected = "RemoveTagFromNote";

			// Act
			var result = StoredProcedure.RemoveTagFromNote;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

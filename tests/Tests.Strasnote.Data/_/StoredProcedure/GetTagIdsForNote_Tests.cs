// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.StoredProcedure_Tests
{
	public class GetTagIdsForNote_Tests
	{
		[Fact]
		public void Returns_Correct_Procedure_Name()
		{
			// Arrange
			const string expected = "GetTagIdsForNote";

			// Act
			var result = StoredProcedure.GetTagIdsForNote;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

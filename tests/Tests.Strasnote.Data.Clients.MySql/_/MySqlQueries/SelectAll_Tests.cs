// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Clients.MySql.MySqlQueries_Tests
{
	public class SelectAll_Tests
	{
		[Fact]
		public void Returns_Correct_Select_All()
		{
			// Arrange
			var queries = new MySqlQueries();
			const string expected = "*";

			// Act
			var result = queries.SelectAll;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

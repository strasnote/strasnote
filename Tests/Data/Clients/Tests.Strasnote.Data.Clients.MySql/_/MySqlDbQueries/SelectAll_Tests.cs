// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlDbQueries_Tests
{
	public class SelectAll_Tests
	{
		[Fact]
		public void Returns_Correct_Select_All()
		{
			// Arrange
			var queries = new MySqlDbQueries();
			const string expected = "*";

			// Act
			var result = queries.SelectAll;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.TypeHandlers.DateTimeTypeHandler_Tests
{
	public class Parse_Tests
	{
		[Fact]
		public void Returns_DateTime_As_UTC()
		{
			// Arrange
			var handler = new DateTimeTypeHandler();
			var date = new DateTime(
				year: Rnd.RndNumber.GetInt32(1000, 5000),
				month: Rnd.RndNumber.GetInt32(3, 8),
				day: Rnd.RndNumber.GetInt32(5, 20),
				hour: Rnd.RndNumber.GetInt32(5, 20),
				minute: Rnd.RndNumber.GetInt32(5, 20),
				second: Rnd.RndNumber.GetInt32(5, 20)
			);
			var input = date.ToLocalTime().ToString();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Equal(DateTimeKind.Utc, result.Kind);
		}

		[Theory]
		[InlineData(null)]
		[InlineData(42)]
		[InlineData("fake date")]
		public void Throws_InvalidCastException_For_Invalid_DateTime(object input)
		{
			// Arrange
			var handler = new DateTimeTypeHandler();

			// Act
			void action() => handler.Parse(input);

			// Assert
			Assert.Throws<InvalidCastException>(action);
		}
	}
}

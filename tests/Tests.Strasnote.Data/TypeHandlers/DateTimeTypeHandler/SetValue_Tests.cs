// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;

namespace Strasnote.Data.TypeHandlers.DateTimeTypeHandler_Tests
{
	public class SetValue_Tests
	{
		[Fact]
		public void Sets_Value_To_UTC()
		{
			// Arrange
			var handler = new DateTimeTypeHandler();
			var parameter = Substitute.For<IDbDataParameter>();
			var hour = Rnd.RndNumber.GetInt32(0, 23);
			var offset = Rnd.RndNumber.GetInt32(4, 16);
			var value = new DateTimeOffset(
				year: Rnd.RndNumber.GetInt32(2, 9998),
				month: Rnd.RndNumber.GetInt32(1, 12),
				day: Rnd.RndNumber.GetInt32(1, 28),
				hour: hour,
				minute: Rnd.RndNumber.GetInt32(0, 59),
				second: Rnd.RndNumber.GetInt32(0, 59),
				TimeSpan.FromHours(offset)
			);

			// Act
			handler.SetValue(parameter, value.LocalDateTime);

			// Assert
			var date = Assert.IsType<DateTime>(parameter.Value);
			Assert.Equal(DateTimeKind.Utc, date.Kind);
			Assert.NotEqual(hour, date.Hour);
		}
	}
}

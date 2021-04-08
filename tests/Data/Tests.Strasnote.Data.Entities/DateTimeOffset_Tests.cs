// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Linq;
using Strasnote.Data.Entities;
using Xunit;

namespace Strasnote.Data.Entities_Tests
{
	public class DateTimeOffset_Tests
	{
		[Fact]
		public void No_DateTimeOffset_Properties()
		{
			// Arrange
			var properties = from t in typeof(IgnoreAttribute).Assembly.GetTypes()
							 from p in t.GetProperties()
							 select new
							 {
								 Entity = t.Name,
								 Property = p.PropertyType
							 };

			// Act
			var result = properties.Where(p => p.Property == typeof(DateTimeOffset));

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity), x.Entity)
			);
		}
	}
}

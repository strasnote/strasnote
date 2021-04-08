// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Strasnote.Data.Entities;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Entities_Tests
{
	public class DateTimeUtc_Tests
	{
		static private IEnumerable<EntityWithDateTimeProperty> GetEntitiesWithDateTimeProperties(Func<Type, bool> where) =>
			from t in typeof(IgnoreAttribute).Assembly.GetTypes()
			from p in t.GetProperties()
			where p.PropertyType == typeof(DateTime) && @where(t)
			select new EntityWithDateTimeProperty(
				Entity: Activator.CreateInstance(t),
				Property: p
			);

		static private (DateTime local, DateTime utc) GetRandomDateTime()
		{
			var local = new DateTimeOffset(
				year: Rnd.RndNumber.GetInt32(0, 9999),
				month: Rnd.RndNumber.GetInt32(1, 12),
				day: Rnd.RndNumber.GetInt32(1, 28),
				hour: Rnd.RndNumber.GetInt32(0, 23),
				minute: Rnd.RndNumber.GetInt32(0, 59),
				second: Rnd.RndNumber.GetInt32(0, 59),
				offset: TimeSpan.FromHours(Rnd.RndNumber.GetInt32(1, 12))
			).LocalDateTime;
			var utc = local.ToUniversalTime();

			return (local, utc);
		}

		static private List<DateTime> SetGetValues(IEnumerable<EntityWithDateTimeProperty> entities, DateTime localDateTime)
		{
			var results = new List<DateTime>();
			foreach (var item in entities)
			{
				item.Property.SetValue(item.Entity, localDateTime);
				if (item.Property.GetValue(item.Entity) is DateTime dt)
				{
					results.Add(dt);
				}
			}

			return results;
		}

		[Fact]
		public void All_DateTime_Properties_Set_DateTime_To_UTC()
		{
			// Arrange
			var entities = GetEntitiesWithDateTimeProperties(t => typeof(TestEntity) != t);
			var (localDateTime, utcDateTime) = GetRandomDateTime();

			// Act
			var results = SetGetValues(entities, localDateTime);

			// Assert
			Assert.All(results,
				x => Assert.Equal(utcDateTime, x, TimeSpan.Zero)
			);
		}

		[Fact]
		public void Test_Entity_DateTime_Property_Does_Not_Set_DateTime_To_UTC()
		{
			// Arrange
			var entities = GetEntitiesWithDateTimeProperties(t => typeof(TestEntity) == t);
			var (localDateTime, utcDateTime) = GetRandomDateTime();

			// Act
			var results = SetGetValues(entities, localDateTime);

			// Assert
			Assert.All(results,
				x => Assert.Equal(localDateTime, x, TimeSpan.Zero)
			);
		}

		public sealed record EntityWithDateTimeProperty(object? Entity, PropertyInfo Property);
	}
}

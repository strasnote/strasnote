// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NSubstitute;
using Xunit;

namespace Strasnote.Data.SqlRepository_Tests
{
	public class GetProperties_Tests
	{
		[Fact]
		public void Gets_Matching_Properties()
		{
			// Arrange
			var (repo, _, _, _, _) = SqlRepository_Setup.Get();

			// Act
			var result = repo.GetProperties<WithSomeMatchingProperties>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Bar), x),
				x => Assert.Equal(nameof(TestEntity.Foo), x)
			);
		}

		[Fact]
		public void Ignores_Properties_With_IgnoreAttribute()
		{
			// Arrange
			var (repo, _, _, _, _) = SqlRepository_Setup.Get();

			// Act
			var result = repo.GetProperties<WithIgnoreAttribute>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Foo), x)
			);
		}

		[Fact]
		public void Ignores_Properties_With_Same_Name_But_Different_Type()
		{
			// Arrange
			var (repo, _, _, _, _) = SqlRepository_Setup.Get();

			// Act
			var result = repo.GetProperties<WithSameNameButDifferentType>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Bar), x)
			);
		}

		[Fact]
		public void Returns_SelectAll_If_No_Matching_Properties()
		{
			// Arrange
			var (repo, _, _, _, _) = SqlRepository_Setup.Get();

			// Act
			var result = repo.GetProperties<NoMatchingProperties>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(repo.QueriesTest.SelectAll, x)
			);
		}

		[Fact]
		public void Uses_Cache_To_Return_Properties()
		{
			// Arrange
			var (repo, _, _, _, _) = SqlRepository_Setup.Get();
			var properties = Substitute.For<List<string>>();
			var cache = new ConcurrentDictionary<Type, List<string>>();
			cache.TryAdd(typeof(ForCacheTest), properties);
			repo.SetCache(cache);

			// Act
			var result = repo.GetProperties<ForCacheTest>();
			repo.SetCache(null);

			// Assert
			Assert.Same(properties, result);
		}

		public sealed record WithSomeMatchingProperties(string Foo, int Bar, int DoNotMap);

		public sealed record WithIgnoreAttribute(string Foo, int AlwaysIgnore)
		{
			[Ignore]
			public bool Bar { get; set; }
		}

		public sealed record WithSameNameButDifferentType(int Foo, int Bar);

		public sealed record NoMatchingProperties(string FooDifferent, bool BarDifferent);

		public sealed record ForCacheTest;
	}
}

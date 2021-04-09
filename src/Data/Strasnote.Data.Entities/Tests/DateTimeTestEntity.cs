// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Tests
{
	/// <summary>
	/// This entity only exists to ensure that the test for DateTimeOffset properties works correctly
	/// </summary>
	internal class DateTimeTestEntity : IEntity
	{
		/// <summary>
		/// Unused ID
		/// </summary>
		public long Id { get; init; }

		/// <summary>
		/// Unused test property - see Tests.Strasnote.Data.Entities.DateTimeOffset_Tests
		/// </summary>
		public DateTimeOffset TestOffsetProperty { get; set; }

		/// <summary>
		/// Unused test property - see Tests.Strasnote.Data.Entities.DateTimeUtc_Tests
		/// </summary>
		public DateTime TestUtcProperty { get; set; }
	}
}

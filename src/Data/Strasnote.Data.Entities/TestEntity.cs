// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities
{
	/// <summary>
	/// This entity only exists to ensure that the test for DateTimeOffset properties works correctly
	/// </summary>
	internal class TestEntity : IEntity
	{
		public long Id { get; init; }

		public DateTimeOffset TestProperty { get; set; }
	}
}

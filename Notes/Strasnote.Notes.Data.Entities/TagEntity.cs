// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Tag entity
	/// </summary>
	public sealed record TagEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			TagId;

		/// <summary>
		/// Tag ID
		/// </summary>
		public long TagId { get; init; }
	}
}

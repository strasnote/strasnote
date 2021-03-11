// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Note entity
	/// </summary>
	public sealed record NoteEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			NoteId;

		/// <summary>
		/// Note ID
		/// </summary>
		public long NoteId { get; init; }
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Note Tag entity
	/// </summary>
	public sealed record NoteTagEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			NoteTagId;

		/// <summary>
		/// Note Tag ID
		/// </summary>
		public long NoteTagId { get; init; }

		#region Relationships

		/// <summary>
		/// Note ID
		/// </summary>
		public long NoteId { get; init; }

		/// <summary>
		/// Tag ID
		/// </summary>
		public long TagId { get; init; }

		#endregion
	}
}

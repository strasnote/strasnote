// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Folder entity
	/// </summary>
	public sealed record FolderEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			FolderId;

		/// <summary>
		/// Folder ID
		/// </summary>
		public long FolderId { get; init; }
	}
}

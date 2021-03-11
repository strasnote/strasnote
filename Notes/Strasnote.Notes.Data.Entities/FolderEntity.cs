// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Folder Name
		/// </summary>
		public string FolderName { get; init; } = string.Empty;

		/// <summary>
		/// Folder ID (of parent Folder)
		/// </summary>
		public int FolderParentId { get; init; }

		/// <summary>
		/// True if this is the User's default Folder
		/// </summary>
		public bool FolderIsDefault { get; init; }

		/// <summary>
		/// When the Folder was created
		/// </summary>
		public DateTimeOffset FolderCreated { get; init; }

		/// <summary>
		/// When the Folder was updated
		/// </summary>
		public DateTimeOffset FolderUpdated { get; init; }

		#region Relationships

		/// <summary>
		/// The ID of the User who owns this Folder
		/// </summary>
		public long UserId { get; init; }

		#endregion

		#region Lookups

		/// <summary>
		/// The list of Notes contained in this Folder
		/// </summary>
		public List<TagEntity>? Notes { get; set; }

		#endregion
	}
}

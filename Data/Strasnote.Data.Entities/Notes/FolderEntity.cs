// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Data.Entities.Notes
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
		/// Folder ID (of parent Folder) - 0 means it is the default
		/// </summary>
		public int FolderParentId { get; init; }

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
		/// The User this Folder belongs to
		/// </summary>
		public UserEntity? User { get; set; }

		/// <summary>
		/// The list of Users who have access to this Folder (owner - User ID - has access by default)
		/// </summary>
		public List<FolderUserEntity>? FolderUsers { get; set; }

		/// <summary>
		/// The list of Notes contained in this Folder
		/// </summary>
		public List<TagEntity>? Notes { get; set; }

		#endregion
	}
}

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
		[Ignore]
		public long Id
		{
			get => FolderId;
			init => FolderId = value;
		}

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
		public DateTime FolderCreated
		{
			get =>
				folderCreated;
			init =>
				folderCreated = value.ToUniversalTime();
		}

		private DateTime folderCreated;

		/// <summary>
		/// When the Folder was updated
		/// </summary>
		public DateTime FolderUpdated
		{
			get =>
				folderUpdated;
			init =>
				folderUpdated = value.ToUniversalTime();
		}

		private DateTime folderUpdated;

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
		[Ignore]
		public UserEntity? User { get; set; }

		/// <summary>
		/// The list of Users who have access to this Folder (owner - User ID - has access by default)
		/// </summary>
		[Ignore]
		public List<FolderUserEntity>? FolderUsers { get; set; }

		/// <summary>
		/// The list of Notes contained in this Folder
		/// </summary>
		[Ignore]
		public List<TagEntity>? Notes { get; set; }

		#endregion
	}
}

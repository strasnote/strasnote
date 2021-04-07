// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Folder User entity
	/// </summary>
	public sealed record FolderUserEntity : IEntity
	{
		/// <inheritdoc/>
		[Ignore]
		public long Id
		{
			get => FolderUserId;
			init => FolderUserId = value;
		}

		/// <summary>
		/// Folder User ID
		/// </summary>
		public long FolderUserId { get; init; }

		/// <summary>
		/// [Optional] Expiry date of the User's access to this Folder
		/// </summary>
		public DateTime? FolderUserExpiry
		{
			get =>
				folderUserExpiry;
			init =>
				folderUserExpiry = value?.ToUniversalTime();
		}

		private DateTime? folderUserExpiry;

		#region Relationships

		/// <summary>
		/// Folder ID
		/// </summary>
		public long FolderId { get; init; }

		/// <summary>
		/// User ID
		/// </summary>
		public long UserId { get; init; }

		#endregion
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Folder User entity
	/// </summary>
	public sealed record FolderUserEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			FolderUserId;

		/// <summary>
		/// Folder User ID
		/// </summary>
		public long FolderUserId { get; init; }

		/// <summary>
		/// [Optional] Expiry date of the User's access to this Folder
		/// </summary>
		public DateTimeOffset? FolderUserExpiry { get; init; }

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

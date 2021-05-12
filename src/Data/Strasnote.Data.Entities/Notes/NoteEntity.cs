// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Note entity
	/// </summary>
	public sealed record NoteEntity : IEntity
	{
		/// <summary>
		/// Note ID
		/// </summary>
		public long Id { get; init; }

		/// <summary>
		/// Note Content (may be encrypted)
		/// </summary>
		public string NoteContent { get; init; } = string.Empty;

		/// <summary>
		/// When the Note was created
		/// </summary>
		public DateTime NoteCreated
		{
			get =>
				noteCreated;
			init =>
				noteCreated = value.ToUniversalTime();
		}

		private DateTime noteCreated;

		/// <summary>
		/// When the Note was updated
		/// </summary>
		public DateTime NoteUpdated
		{
			get =>
				noteUpdated;
			init =>
				noteUpdated = value.ToUniversalTime();
		}

		private DateTime noteUpdated;

		#region Relationships

		/// <summary>
		/// The ID of the Folder this Note is in
		/// </summary>
		public long FolderId { get; init; }

		/// <summary>
		/// The ID of the User who owns this Note
		/// </summary>
		public long UserId { get; init; }

		#endregion

		#region Lookups

		/// <summary>
		/// The Folder this Note belongs to
		/// </summary>
		[Ignore]
		public FolderEntity? Folder { get; set; }

		/// <summary>
		/// The User this Note belongs to
		/// </summary>
		[Ignore]
		public UserEntity? User { get; set; }

		/// <summary>
		/// The list of Users who have access to this Note
		/// </summary>
		[Ignore]
		public List<NoteUserEntity>? NoteUsers { get; set; }

		/// <summary>
		/// The list of Tags this Note has
		/// </summary>
		[Ignore]
		public List<TagEntity>? Tags { get; set; }

		#endregion
	}
}

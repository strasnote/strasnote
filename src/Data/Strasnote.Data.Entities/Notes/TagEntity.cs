// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Tag entity
	/// </summary>
	public sealed record TagEntity : IEntity
	{
		/// <summary>
		/// Tag ID
		/// </summary>
		public long Id { get; init; }

		/// <summary>
		/// Tag Name
		/// </summary>
		public string TagName { get; init; } = string.Empty;

		/// <summary>
		/// The Tag Name stripped of spaces and special characters
		/// </summary>
		public string TagNameNormalised { get; init; } = string.Empty;

		/// <summary>
		/// When the Tag was created
		/// </summary>
		public DateTime TagCreated
		{
			get =>
				tagCreated;
			init =>
				tagCreated = value.ToUniversalTime();
		}

		private DateTime tagCreated;

		/// <summary>
		/// When the Tag was last updated
		/// </summary>
		public DateTime TagUpdated
		{
			get =>
				tagUpdated;
			init =>
				tagUpdated = value.ToUniversalTime();
		}

		private DateTime tagUpdated;

		#region Relationships

		/// <summary>
		/// User ID (of the User who owns this Tag)
		/// </summary>
		public long UserId { get; init; }

		#endregion

		#region Lookups

		/// <summary>
		/// The User this Tag belongs to
		/// </summary>
		[Ignore]
		public UserEntity? User { get; set; }

		/// <summary>
		/// List of notes with this Tag
		/// </summary>
		[Ignore]
		public List<NoteEntity>? Notes { get; set; }

		#endregion
	}
}

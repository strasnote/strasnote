// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Note User entity
	/// </summary>
	public sealed record NoteUserEntity : IEntity
	{
		/// <inheritdoc/>
		[Ignore]
		public long Id
		{
			get => NoteUserId;
			init => NoteUserId = value;
		}

		/// <summary>
		/// Note User ID
		/// </summary>
		public long NoteUserId { get; init; }

		/// <summary>
		/// [Optional] Expiry date of the User's access to this Note
		/// </summary>
		public DateTimeOffset? NoteUserExpiry { get; init; }

		#region Relationships

		/// <summary>
		/// Note ID
		/// </summary>
		public long NoteId { get; init; }

		/// <summary>
		/// User ID
		/// </summary>
		public long UserId { get; init; }

		#endregion
	}
}

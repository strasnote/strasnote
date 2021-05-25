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
		/// <summary>
		/// Note User ID
		/// </summary>
		public long Id { get; init; }

		/// <summary>
		/// [Optional] Expiry date of the User's access to this Note
		/// </summary>
		public DateTime? NoteUserExpiry
		{
			get =>
				noteUserExpiry;
			init =>
				noteUserExpiry = value?.ToUniversalTime();
		}

		private DateTime? noteUserExpiry;

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

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Entities.Notes.Enums;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Enrypted entity
	/// </summary>
	public sealed record EncryptedEntity : IEntityWithUserId
	{
		/// <summary>
		/// Encrypted ID
		/// </summary>
		public ulong Id { get; init; }

		/// <summary>
		/// Encrypted Object (e.g. Folder / Note) ID
		/// </summary>
		public ulong EncryptedObjectId { get; init; }

		/// <summary>
		/// Encrypted Object Type (e.g. Folder / Note)
		/// </summary>
		public EncryptedObject EncryptedObjectType { get; init; }

		/// <summary>
		/// The Encryption Key for this object (encryted using this User's Public Key)
		/// </summary>
		public string EncryptedKeyHash { get; init; } = string.Empty;

		/// <summary>
		/// [Optional] Expiry date of this encryption key
		/// </summary>
		public DateTime? EncryptedExpiry
		{
			get =>
				encryptedExpiry;
			init =>
				encryptedExpiry = value?.ToUniversalTime();
		}

		private DateTime? encryptedExpiry;

		#region Relationships

		/// <summary>
		/// The ID of the User this Encrypted item belongs to
		/// </summary>
		public ulong UserId { get; init; }

		#endregion

		#region Lookups

		/// <summary>
		/// The User this Encrypted item belongs to
		/// </summary>
		[Ignore]
		public UserEntity? User { get; set; }

		#endregion
	}
}

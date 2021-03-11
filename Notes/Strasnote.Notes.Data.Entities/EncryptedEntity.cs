// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Enrypted entity
	/// </summary>
	public sealed record EncryptedEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			EnryptedId;

		/// <summary>
		/// Enryption ID
		/// </summary>
		public long EnryptedId { get; init; }

		/// <summary>
		/// Encrypted Object (e.g. Folder / Note) ID
		/// </summary>
		public long EncryptedObjectId { get; init; }

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
		public DateTimeOffset? EncryptedExpiry { get; init; }

		#region Relationships

		/// <summary>
		/// The ID of the User this Encrypted item belongs to
		/// </summary>
		public long UserId { get; init; }

		#endregion
	}
}

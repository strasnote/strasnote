// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public sealed class UserEntity : Auth.Data.Entities.UserEntity, IEntity
	{
		/// <summary>
		/// User ID (alias for <see cref="Microsoft.AspNetCore.Identity.IdentityUser{TKey}.Id"/>)
		/// </summary>
		public long UserId
		{
			get => Id;
			set => Id = value;
		}

		/// <summary>
		/// The User's given name (or Christian / first name)
		/// </summary>
		public string UserGivenName { get; init; } = string.Empty;

		/// <summary>
		/// The User's full name
		/// </summary>
		public string UserFullName { get; init; } = string.Empty;

		/// <summary>
		/// This User's Public Key (for encryption)
		/// </summary>
		public string UserPublicKey { get; set; } = string.Empty;

		/// <summary>
		/// This User's Private Key (for decryption - encrypted using User's password)
		/// </summary>
		public string UserPrivateKey { get; set; } = string.Empty;

		#region Lookups

		/// <summary>
		/// The list of roles this User has
		/// </summary>
		public List<RoleEntity>? Roles { get; set; }

		/// <summary>
		/// List of folders owned by this User
		/// </summary>
		public List<FolderEntity>? Folders { get; set; }

		/// <summary>
		/// List of notes owned by this User
		/// </summary>
		public List<NoteEntity>? Notes { get; set; }

		/// <summary>
		/// List of tags owned by this User
		/// </summary>
		public List<TagEntity>? Tags { get; set; }

		/// <summary>
		/// Folder encryption keys
		/// </summary>
		public List<EncryptedEntity>? FolderEncryptionKeys { get; set; }

		/// <summary>
		/// Note encryption keys
		/// </summary>
		public List<EncryptedEntity>? NoteEncryptionKeys { get; set; }

		#endregion
	}
}

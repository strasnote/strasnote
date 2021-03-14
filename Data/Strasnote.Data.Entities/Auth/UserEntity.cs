﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Data.Entities.Auth
{
	public class UserEntity : IdentityUser<long>
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
		/// This User's Public Key (for encryption)
		/// </summary>
		public string UserPublicKey { get; set; } = string.Empty;

		/// <summary>
		/// This User's Private Key (for decryption - encrypted using User's password)
		/// </summary>
		public string UserPrivateKey { get; set; } = string.Empty;

		/// <summary>
		/// User Profile information (e.g. name)
		/// </summary>
		public Profile UserProfile { get; init; } = new();

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

		/// <summary>
		/// User Profile information
		/// </summary>
		/// <param name="UserGivenName">The User's given name (or Christian / first name)</param>
		/// <param name="UserFullName">The User's full name</param>
		public record Profile(string? UserGivenName, string? UserFullName)
		{
			/// <summary>
			/// Create with blank details
			/// </summary>
			public Profile() : this(null, null) { }
		}
	}
}
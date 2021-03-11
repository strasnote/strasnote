// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Role entity
	/// </summary>
	public sealed class RoleEntity : Auth.Data.Entities.RoleEntity, IEntity
	{
		/// <summary>
		/// Role ID (alias for <see cref="Microsoft.AspNetCore.Identity.IdentityRole{TKey}.Id"/>)
		/// </summary>
		public long RoleId
		{
			get => Id;
			set => Id = value;
		}

		#region Lookups

		/// <summary>
		/// List of users with this Role
		/// </summary>
		public List<UserEntity>? Users { get; set; }

		#endregion
	}
}

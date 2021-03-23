// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Auth
{
	/// <inheritdoc cref="IdentityRole{TKey}"/>
	public class RoleEntity : IdentityRole<long>, IEntity
	{
		/// <summary>
		/// Role ID (alias for <see cref="IdentityRole{TKey}.Id"/>)
		/// </summary>
		[Ignore]
		public long RoleId
		{
			get => Id;
			set => Id = value;
		}

		#region Lookups

		/// <summary>
		/// List of users with this Role
		/// </summary>
		[Ignore]
		public List<UserEntity>? Users { get; set; }

		#endregion
	}
}

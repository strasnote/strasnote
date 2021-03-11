// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
	}
}

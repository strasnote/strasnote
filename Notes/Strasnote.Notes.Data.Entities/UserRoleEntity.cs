// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// User Role entity
	/// </summary>
	public sealed record UserRoleEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			UserRoleId;

		/// <summary>
		/// User Role ID
		/// </summary>
		public long UserRoleId { get; init; }

		#region Relationships

		/// <summary>
		/// User ID
		/// </summary>
		public long UserId { get; init; }

		/// <summary>
		/// Role ID
		/// </summary>
		public long RoleId { get; init; }

		#endregion
	}
}

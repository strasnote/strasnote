// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Role entity
	/// </summary>
	public sealed record RoleEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			RoleId;

		/// <summary>
		/// Role ID
		/// </summary>
		public long RoleId { get; init; }
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Interface for database entities with a User ID
	/// </summary>
	public interface IEntityWithUserId : IEntity
	{
		/// <summary>
		/// User ID
		/// </summary>
		ulong UserId { get; }
	}
}

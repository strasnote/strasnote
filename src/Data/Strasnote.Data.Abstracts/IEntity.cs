// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Interface for database entities
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Entity ID
		/// </summary>
		long Id { get; }
	}
}

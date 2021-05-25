// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with tags in the database
	/// </summary>
	public interface ITagRepository : IRepository<TagEntity>
	{
	}
}

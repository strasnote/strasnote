// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with note tags in the database
	/// </summary>
	public interface INoteTagRepository : IRepository<NoteTagEntity>
	{
	}
}

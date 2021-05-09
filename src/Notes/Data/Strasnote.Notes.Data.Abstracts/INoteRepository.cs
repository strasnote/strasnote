// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the note database
	/// </summary>
	public interface INoteRepository : IRepository<NoteEntity>
	{
	}
}

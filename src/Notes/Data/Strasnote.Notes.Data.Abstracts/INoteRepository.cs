// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with notes in the database
	/// </summary>
	public interface INoteRepository : IRepository<NoteEntity>
	{
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with notes in the database
	/// </summary>
	public interface IFolderRepository : IRepository<FolderEntity>
	{
	}
}

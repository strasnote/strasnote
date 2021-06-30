// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Notes.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with tags in the database
	/// </summary>
	public interface ITagRepository : IRepository<TagEntity>
	{
		/// <summary>
		/// Get tags for the specified note
		/// </summary>
		/// <typeparam name="TTag">Tag model type</typeparam>
		/// <param name="noteId">Note ID</param>
		/// <param name="userId">User ID</param>
		Task<IEnumerable<TTag>> GetForNote<TTag>(ulong noteId, ulong? userId);
	}
}

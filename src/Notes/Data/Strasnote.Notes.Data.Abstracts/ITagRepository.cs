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
		/// Add a tag to the specified note
		/// </summary>
		/// <param name="tagId">Tag ID</param>
		/// <param name="noteId">Note ID</param>
		/// <param name="userId">User ID</param>
		Task<bool> AddToNote(ulong tagId, ulong noteId, ulong userId);

		/// <summary>
		/// Get tags for the specified note
		/// </summary>
		/// <typeparam name="TTag">Tag model type</typeparam>
		/// <param name="noteId">Note ID</param>
		/// <param name="userId">User ID</param>
		Task<IEnumerable<TTag>> GetForNote<TTag>(ulong noteId, ulong userId);

		/// <summary>
		/// Remove a tag from the specified note
		/// </summary>
		/// <param name="tagId">Tag ID</param>
		/// <param name="noteId">Note ID</param>
		/// <param name="userId">User ID</param>
		Task<bool> RemoveFromNote(ulong tagId, ulong noteId, ulong userId);
	}
}

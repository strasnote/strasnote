// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strasnote.Notes.Storage.Abstracts
{
	/// <summary>
	/// Abstract note storage
	/// </summary>
	public interface INoteStore
	{
		/// <summary>
		/// Create a new note
		/// </summary>
		/// <param name="note"></param>
		Task<bool> CreateAsync(Note note);

		/// <summary>
		/// Save a note
		/// </summary>
		/// <param name="note"></param>
		Task<bool> SaveAsync(Note note);

		/// <summary>
		/// Delete a note
		/// </summary>
		/// <param name="notePath"></param>
		Task<bool> DeleteAsync(string notePath);

		/// <summary>
		/// Delete a note
		/// </summary>
		/// <param name="note"></param>
		Task<bool> DeleteAsync(Note note);

		/// <summary>
		/// Get the note at the specified path
		/// </summary>
		/// <param name="notePath"></param>
		Task<Note> GetAsync(string notePath);

		/// <summary>
		/// Get all notes in the specified path
		/// </summary>
		/// <param name="folderPath"></param>
		/// <param name="recursive"></param>
		Task<IEnumerable<Note>> GetAsync(string folderPath, bool recursive);
	}
}

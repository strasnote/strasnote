// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Notes.Storage.Abstracts;

namespace Strasnote.Notes.Storage
{
	/// <inheritdoc/>
	public abstract class NoteStore : INoteStore
	{
		/// <inheritdoc/>
		public virtual Task<bool> CreateAsync(Note note)
		{
			// do something

			return DoCreateAsync(note);
		}

		protected abstract Task<bool> DoCreateAsync(Note note);

		/// <inheritdoc/>
		public virtual Task<bool> SaveAsync(Note note)
		{
			// do something

			return DoSaveAsync(note);
		}

		protected abstract Task<bool> DoSaveAsync(Note note);

		/// <inheritdoc/>
		public virtual Task<bool> DeleteAsync(string path)
		{
			// do something

			return DoDeleteAsync(path);
		}

		protected abstract Task<bool> DoDeleteAsync(string path);

		/// <inheritdoc/>
		public virtual Task<bool> DeleteAsync(Note note)
		{
			// do something

			return DoDeleteAsync(note);
		}

		protected abstract Task<bool> DoDeleteAsync(Note note);

		/// <inheritdoc/>
		public virtual Task<Note> GetAsync(string path)
		{
			// do something

			return DoGetAsync(path);
		}

		protected abstract Task<Note> DoGetAsync(string path);

		/// <inheritdoc/>
		public virtual Task<IEnumerable<Note>> GetAsync(string path, bool recursive)
		{
			// do something

			return DoGetAsync(path, recursive);
		}

		protected abstract Task<IEnumerable<Note>> DoGetAsync(string path, bool recursive);
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Notes.Storage.Abstracts;

namespace Strasnote.Notes.Storage.FileSystem
{
	/// <summary>
	/// Note Storage in the file system
	/// </summary>
	public sealed class FileSystemNoteStore : NoteStore
	{
		protected override Task<bool> DoCreateAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<Note> DoGetAsync(string path) => throw new System.NotImplementedException();
		protected override Task<IEnumerable<Note>> DoGetAsync(string path, bool recursive) => throw new System.NotImplementedException();
		protected override Task<bool> DoSaveAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(string path) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(Note note) => throw new System.NotImplementedException();
	}
}

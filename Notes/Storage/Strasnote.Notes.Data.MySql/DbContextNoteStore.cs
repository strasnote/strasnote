// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Storage.Abstracts;

namespace Strasnote.Notes.Storage.DbContext
{
	/// <summary>
	/// Note Storage in a database
	/// </summary>
	public sealed class DbContextNoteStore : NoteStore
	{
		private readonly IDbContext<NoteEntity> dbContext;

		private readonly ILog log;

		public DbContextNoteStore(IDbContext<NoteEntity> dbContext, ILog<DbContextNoteStore> log) =>
			(this.dbContext, this.log) = (dbContext, log);

		protected override Task<bool> DoCreateAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<Note> DoGetAsync(string path) => throw new System.NotImplementedException();
		protected override Task<IEnumerable<Note>> DoGetAsync(string path, bool recursive) => throw new System.NotImplementedException();
		protected override Task<bool> DoSaveAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(string path) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(Note note) => throw new System.NotImplementedException();
	}
}

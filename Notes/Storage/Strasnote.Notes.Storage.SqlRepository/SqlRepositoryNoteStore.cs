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
	public sealed class SqlRepositoryNoteStore : NoteStore
	{
		private readonly ISqlRepository<NoteEntity> repository;

		private readonly ILog log;

		public SqlRepositoryNoteStore(ISqlRepository<NoteEntity> dbContext, ILog<SqlRepositoryNoteStore> log) =>
			(this.repository, this.log) = (dbContext, log);

		protected override Task<bool> DoCreateAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<Note> DoGetAsync(string path) => throw new System.NotImplementedException();
		protected override Task<IEnumerable<Note>> DoGetAsync(string path, bool recursive) => throw new System.NotImplementedException();
		protected override Task<bool> DoSaveAsync(Note note) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(string path) => throw new System.NotImplementedException();
		protected override Task<bool> DoDeleteAsync(Note note) => throw new System.NotImplementedException();
	}
}

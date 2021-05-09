// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Data
{
	/// <inheritdoc cref="INoteRepository"/>
	public sealed class NoteSqlRepository : SqlRepository<NoteEntity>, INoteRepository
	{
		/// <summary>
		/// Inject objects
		/// </summary>
		/// <param name="client">ISqlClient</param>
		/// <param name="log">ILog</param>
		public NoteSqlRepository(ISqlClient client, ILog<NoteSqlRepository> log) : base(client, log, client.Tables.Note) { }
	}
}

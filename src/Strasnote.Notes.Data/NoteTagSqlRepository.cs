// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Data
{
	/// <inheritdoc cref="INoteTagRepository"/>
	public sealed class NoteTagSqlRepository : SqlRepository<NoteTagEntity>, INoteTagRepository
	{
		/// <summary>
		/// Inject objects
		/// </summary>
		/// <param name="client">ISqlClient</param>
		/// <param name="log">ILog</param>
		public NoteTagSqlRepository(ISqlClient client, ILog<NoteSqlRepository> log) : base(client, log, client.Tables.NoteTag) { }
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrate
{
	public static class TestNote
	{
		/// <summary>
		/// Insert test notes from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">INoteRepository</param>
		/// <param name="folderId">Folder ID</param>
		public static async Task InsertAsync(ILog log, INoteRepository repo, ulong folderId)
		{
			var noteId = await repo.CreateAsync(new NoteEntity
			{
				UserId = 1,
				FolderId = folderId,
				NoteContent = "This is a test note."
			}).ConfigureAwait(false);

			log.Debug("Inserted test note {NoteId}.", noteId);
		}
	}
}

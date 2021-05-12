// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
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
		public static async Task InsertAsync(ILog log, INoteRepository repo)
		{
			var noteId = await repo.CreateAsync(new()
			{
				UserId = 1,
				FolderId = 0,
				NoteContent = "This is a test note."
			}).ConfigureAwait(false);

			log.Debug("Inserted test note {NoteId}.", noteId);
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrate
{
	public static class TestNoteTag
	{
		/// <summary>
		/// Insert test tags from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">INoteTagRepository</param>
		public static async Task InsertAsync(ILog log, INoteTagRepository repo, ulong noteId, ulong tagId)
		{
			var noteTagId = await repo.CreateAsync(new()
			{
				NoteId = noteId,
				TagId = tagId
			}).ConfigureAwait(false);

			log.Debug("Inserted test note tag {Tag}.", noteTagId);
		}
	}
}

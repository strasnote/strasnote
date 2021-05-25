// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrate
{
	public static class TestFolder
	{
		/// <summary>
		/// Insert test folders from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">IFolderRepository</param>
		public static async Task<long> InsertAsync(ILog log, IFolderRepository repo)
		{
			var folderId = await repo.CreateAsync(new()
			{
				UserId = 1,
				FolderName = "Main folder"
			}).ConfigureAwait(false);

			log.Debug("Inserted test folder {Folder}.", folderId);

			return folderId;
		}
	}
}

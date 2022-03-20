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
		/// <param name="userId">User ID</param>
		public static async Task<ulong> InsertAsync(ILog log, IFolderRepository repo, ulong userId)
		{
			var folderId = await repo.CreateAsync(new()
			{
				UserId = userId,
				FolderName = "Main folder"
			}).ConfigureAwait(false);

			log.Debug("Inserted test folder {Folder}.", folderId);

			return folderId;
		}
	}
}

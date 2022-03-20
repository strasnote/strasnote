// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrate
{
	public static class TestTag
	{
		/// <summary>
		/// Insert test tags from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">ITagRepository</param>
		/// <param name="userId">User ID</param>
		public static async Task<ulong> InsertAsync(ILog log, ITagRepository repo, ulong userId)
		{
			const string tag0Name = "1. Tag Tester!";
			var tag0 = await repo.CreateAsync(new()
			{
				UserId = userId,
				TagName = tag0Name
			}).ConfigureAwait(false);

			log.Debug("Inserted test tag {Tag}.", tag0);

			const string tag1Name = "Linked tag";
			var tag1 = await repo.CreateAsync(new()
			{
				UserId = userId,
				TagName = tag1Name
			}).ConfigureAwait(false);

			log.Debug("Inserted test tag {Tag}.", tag1);

			return tag1;
		}
	}
}

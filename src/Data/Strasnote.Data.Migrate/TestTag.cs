// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Data.Migrate
{
	public static class TestTag
	{
		/// <summary>
		/// Insert test tags from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">ITagRepository</param>
		public static async Task<long> InsertAsync(ILog log, ITagRepository repo)
		{
			const string name = "1. Tag Tester!";
			var tagId = await repo.CreateAsync(new()
			{
				UserId = 1,
				TagName = name,
				TagNameNormalised = name.Normalise()
			}).ConfigureAwait(false);

			log.Debug("Inserted test tag {Tag}.", tagId);

			return tagId;
		}
	}
}

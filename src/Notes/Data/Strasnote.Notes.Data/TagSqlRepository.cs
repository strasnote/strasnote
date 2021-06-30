// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Notes.Data
{
	/// <inheritdoc cref="ITagRepository"/>
	public sealed class TagSqlRepository : SqlRepository<TagEntity>, ITagRepository
	{
		/// <summary>
		/// Inject objects
		/// </summary>
		/// <param name="client">ISqlClient</param>
		/// <param name="log">ILog</param>
		public TagSqlRepository(ISqlClient client, ILog<TagSqlRepository> log) : base(client, log, client.Tables.Tag) { }

		/// <inheritdoc/>
		public override Task<ulong> CreateAsync(TagEntity entity) =>
			base.CreateAsync(entity with
			{
				TagNameNormalised = entity.TagName.Normalise(),
				TagCreated = DateTime.Now,
				TagUpdated = DateTime.Now
			});

		/// <inheritdoc/>
		public async Task<IEnumerable<TTag>> GetForNote<TTag>(ulong noteId, ulong? userId)
		{
			// Create new connection
			using var connection = Client.Connect();

			// Get tag IDs for this note
			var ids = await connection.QueryAsync<ulong>(
				sql: StoredProcedure.GetTagIdsForNote,
				param: new { noteId, userId },
				commandType: CommandType.StoredProcedure
			).ConfigureAwait(false);

			// If there are no Tags, return empty list
			if (!ids.Any())
			{
				return new List<TTag>();
			}

			// Get matching tags
			return await QueryAsync<TTag>(
				userId: null,
				(t => t.Id, SearchOperator.In, ids)
			).ConfigureAwait(false);
		}
	}
}

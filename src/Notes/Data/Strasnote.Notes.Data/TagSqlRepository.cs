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

		#region CRUD overrides

		/// <inheritdoc/>
		public override Task<ulong> CreateAsync(TagEntity entity) =>
			base.CreateAsync(entity with
			{
				TagNameNormalised = entity.TagName.Normalise(),
				TagCreated = DateTime.Now,
				TagUpdated = DateTime.Now
			});

		/// <inheritdoc/>
		public override async Task<int> DeleteAsync(ulong id, ulong? userId)
		{
			// Create new connection
			using var connection = Client.Connect();

			// Remove tag from all notes
			var result = await connection.ExecuteScalarAsync<int>(
				sql: StoredProcedure.RemoveTagFromNotes,
				param: new { tagId = id, userId },
				commandType: CommandType.StoredProcedure
			).ConfigureAwait(false);

			// Use defaul method to delete the note itself
			return await base.DeleteAsync(id, userId).ConfigureAwait(false);
		}

		#endregion

		#region Additional operations

		/// <inheritdoc/>
		public async Task<bool> AddToNote(ulong tagId, ulong noteId, ulong userId)
		{
			// Create new connection
			using var connection = Client.Connect();

			// Add tag to the note
			var result = await connection.ExecuteScalarAsync<int>(
				sql: StoredProcedure.AddTagToNote,
				param: new { tagId, noteId, userId },
				commandType: CommandType.StoredProcedure
			).ConfigureAwait(false);

			// Return result
			return result == 1;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<TTag>> GetForNote<TTag>(ulong noteId, ulong userId)
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

		/// <inheritdoc/>
		public async Task<bool> RemoveFromNote(ulong tagId, ulong noteId, ulong userId)
		{
			// Create new connection
			using var connection = Client.Connect();

			// Remove tag from the note
			var result = await connection.ExecuteScalarAsync<int>(
				sql: StoredProcedure.RemoveTagFromNote,
				param: new { tagId, noteId, userId },
				commandType: CommandType.StoredProcedure
			).ConfigureAwait(false);

			// Return result
			return result == 0;
		}

		#endregion
	}
}

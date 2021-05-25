// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

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
		public override Task<long> CreateAsync(TagEntity entity) =>
			base.CreateAsync(entity with
			{
				TagCreated = DateTime.Now,
				TagUpdated = DateTime.Now
			});
	}
}

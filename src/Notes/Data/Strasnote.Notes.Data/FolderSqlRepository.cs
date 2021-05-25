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
	/// <inheritdoc cref="IFolderRepository"/>
	public sealed class FolderSqlRepository : SqlRepository<FolderEntity>, IFolderRepository
	{
		/// <summary>
		/// Inject objects
		/// </summary>
		/// <param name="client">ISqlClient</param>
		/// <param name="log">ILog</param>
		public FolderSqlRepository(ISqlClient client, ILog<NoteSqlRepository> log) : base(client, log, client.Tables.Folder) { }

		/// <inheritdoc/>
		public override Task<long> CreateAsync(FolderEntity entity) =>
			base.CreateAsync(entity with
			{
				FolderCreated = DateTime.Now,
				FolderUpdated = DateTime.Now
			});
	}
}

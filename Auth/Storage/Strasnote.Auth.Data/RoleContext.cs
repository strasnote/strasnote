// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class RoleContext : DbContextWithQueries<RoleEntity>, IRoleContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog with context</param>
		public RoleContext(IDbClientWithQueries client, ILog<UserContext> log)
			: base(client, log, client.Tables.Role) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByNameAsync<TModel>(string roleName) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<IEnumerable<TModel>> RetrieveForUserAsync<TModel>(long userId) => throw new NotImplementedException();
	}
}

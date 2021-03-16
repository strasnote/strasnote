// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Strasnote.Auth.Config;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class RoleContext : DbContext<RoleEntity>, IRoleContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="config">AuthConfig</param>
		/// <param name="log">ILog with context</param>
		public RoleContext(IDbClient client, IOptions<AuthConfig> config, ILog<UserContext> log)
			: base(client, config.Value.Db.ConnectionString, log) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByNameAsync<TModel>(string roleName) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<IEnumerable<TModel>> RetrieveForUserAsync<TModel>(long userId) => throw new NotImplementedException();
	}
}

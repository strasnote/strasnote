// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
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
	public sealed class RefreshTokenContext : DbContext<RefreshTokenEntity>, IRefreshTokenContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="config">AuthConfig</param>
		/// <param name="log">ILog with context</param>
		public RefreshTokenContext(IDbClient client, IOptions<AuthConfig> config, ILog log)
			: base(client, config.Value.Db.ConnectionString, log) { }

		/// <inheritdoc/>
		public Task CreateAsync(RefreshTokenEntity entity) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<bool> DeleteByUserIdAsync(long userId) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<RefreshTokenEntity> RetrieveForUserAsync(long userId, string refreshToken) => throw new NotImplementedException();
	}
}

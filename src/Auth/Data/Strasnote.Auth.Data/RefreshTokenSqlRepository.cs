// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class RefreshTokenSqlRepository : SqlRepository<RefreshTokenEntity>, IRefreshTokenRepository
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog with context</param>
		public RefreshTokenSqlRepository(ISqlClient client, ILog log)
			: base(client, log, client.Tables.RefreshToken) { }

		/// <inheritdoc/>
		public Task<bool> DeleteByUserIdAsync(long userId)
		{
			const string sql = @"DELETE 
								 FROM `auth.refresh_token` 
								 WHERE `auth.refresh_token`.UserId = @UserId";

			return Connection.ExecuteScalarAsync<bool>(sql, new { UserId = userId });
		}

		/// <inheritdoc/>
		public Task<RefreshTokenEntity> RetrieveForUserAsync(long userId, string refreshToken) => throw new NotImplementedException();
	}
}

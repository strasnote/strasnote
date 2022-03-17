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
		public async Task<int> DeleteByUserIdAsync(ulong userId)
		{
			// Connect to the database
			using var connection = await Client.ConnectAsync();

			// Custom query
			var sql = $"DELETE FROM `auth.refresh_token` WHERE `auth.refresh_token`.`UserId` = @{nameof(userId)};";

			// Execute query
			return await connection.ExecuteAsync(sql, new { userId }).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public Task<RefreshTokenEntity> RetrieveForUserAsync(ulong userId, string refreshToken) =>
			throw new NotImplementedException();
	}
}

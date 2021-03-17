// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class UserContext : DbContext<UserEntity>, IUserContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog with context</param>
		public UserContext(IDbClient client, ILog<UserContext> log)
			: base(client, log) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByEmailAsync<TModel>(string email)
		{
			// Log retrieve
			LogOperation(nameof(RetrieveByEmailAsync), "{Email}", email);

			// Perform retrieve and map to model
			return Connection.QuerySingleOrDefaultAsync<TModel>(
				sql: GetStoredProcedureName(nameof(RetrieveByEmailAsync)),
				param: new { email },
				commandType: CommandType.StoredProcedure
			);
		}

		/// <inheritdoc/>
		public Task<TModel> RetrieveByUsernameAsync<TModel>(string name) =>
			RetrieveByEmailAsync<TModel>(name);
	}
}

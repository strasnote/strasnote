// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class UserContext : DbContextWithQueries<UserEntity>, IUserContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog with context</param>
		public UserContext(IDbClientWithQueries client, ILog<UserContext> log)
			: base(client, log, client.Tables.User) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByEmailAsync<TModel>(string email) =>
			QuerySingleAsync<TModel>(
				(u => u.Email, SearchOperator.Equal, email)
			);

		/// <inheritdoc/>
		public Task<TModel> RetrieveByUsernameAsync<TModel>(string name) =>
			RetrieveByEmailAsync<TModel>(name);
	}
}

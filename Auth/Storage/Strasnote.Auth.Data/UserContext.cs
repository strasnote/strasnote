// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
	public sealed class UserContext : DbContext<UserEntity>, IUserContext
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="config">AuthConfig</param>
		/// <param name="log">ILog with context</param>
		public UserContext(IDbClient client, IOptions<AuthConfig> config, ILog<UserContext> log)
			: base(client, config.Value.Db.ConnectionString, log) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByEmailAsync<TModel>(string email) => throw new System.NotImplementedException();

		/// <inheritdoc/>
		public Task<TModel> RetrieveByUsernameAsync<TModel>(string name) =>
			RetrieveByEmailAsync<TModel>(name);
	}
}

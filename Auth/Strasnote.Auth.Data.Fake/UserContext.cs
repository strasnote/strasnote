using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IUserContext"/>
	public sealed class UserContext : IUserContext
	{
		private readonly ILog log;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="log">ILog with context</param>
		public UserContext(ILog<UserContext> log) =>
			this.log = log;

		/// <inheritdoc/>
		public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			log.Information("Create user: {@User}", user);
			return Task.FromResult(IdentityResult.Success);
		}

		/// <inheritdoc/>
		public Task<UserEntity> RetrieveAsync(int id, CancellationToken cancellationToken)
		{
			log.Information("Retrieve user with ID: {Id}", id);
			return Task.FromResult(new UserEntity { Id = id });
		}

		/// <inheritdoc/>
		public Task<UserEntity> RetrieveAsync(string name, CancellationToken cancellationToken)
		{
			log.Information("Retrieve user with name: {Name}", name);
			return Task.FromResult(new UserEntity { UserName = name });
		}

		/// <inheritdoc/>
		public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			log.Information("Updating user: {@User}", user);
			return Task.FromResult(IdentityResult.Success);
		}

		/// <inheritdoc/>
		public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
		{
			log.Information("Deleting user: {@User}", user);
			return Task.FromResult(IdentityResult.Success);
		}

		/// <inheritdoc/>
		public void Dispose() =>
			log.Information("Disposing...");
	}
}

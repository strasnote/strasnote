using System;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IRefreshTokenContext"/>
	public sealed class RefreshTokenContext : IRefreshTokenContext
	{
		private readonly ILog log;

		public RefreshTokenContext(ILog<RefreshTokenContext> log) =>
			this.log = log;

		/// <inheritdoc/>
		public Task<RefreshTokenEntity> CreateAsync(RefreshTokenEntity refreshToken)
		{
			log.Information("Create refresh token: {@RefreshToken}", refreshToken);
			return Task.FromResult(new RefreshTokenEntity("token", DateTimeOffset.Now.AddDays(1), 1));
		}

		/// <inheritdoc/>
		public Task<bool> DeleteByUserIdAsync(long userId)
		{
			log.Information("Deleting refresh token for user: {@UserId}", userId);
			return Task.FromResult(true);
		}

		/// <inheritdoc/>
		public Task<RefreshTokenEntity> Retrieve(long userId, string refreshToken)
		{
			log.Information("Retrieve refresh token for user: {@UserId}, refresh token: {@ResfreshToken}", userId, refreshToken);
			return Task.FromResult(new RefreshTokenEntity("token", DateTimeOffset.Now.AddDays(1), 1));
		}
	}
}

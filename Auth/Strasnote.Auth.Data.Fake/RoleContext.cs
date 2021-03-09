// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IRoleContext"/>
	public sealed class RoleContext : IRoleContext
	{
		private readonly ILog<RoleContext> log;

		public RoleContext(ILog<RoleContext> log) =>
			this.log = log;

		/// <inheritdoc/>
		public Task<RoleEntity> RetrieveAsync(string roleName, CancellationToken cancellationToken)
		{
			log.Information("Retrieve role with name: {RoleName}", roleName);
			return Task.FromResult(new RoleEntity { Name = roleName });
		}

		/// <inheritdoc/>
		public async Task<IList<RoleEntity>> RetrieveForUserAsync(long userId)
		{
			log.Information("Retrieve roles for user {UserId}", userId);

			List<RoleEntity> roles = new()
			{
				new RoleEntity
				{
					Id = userId,
					Name = "TestRole"
				}
			};

			return await Task.FromResult(roles).ConfigureAwait(false);
		}
	}
}

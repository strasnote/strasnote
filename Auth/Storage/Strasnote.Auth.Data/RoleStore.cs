// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data
{
	/// <summary>
	/// Handles roles CRUD
	/// Implementation reference: https://github.com/aspnet/AspNetIdentity/blob/master/src/Microsoft.AspNet.Identity.EntityFramework/UserStore.cs
	/// </summary>
	public class RoleStore :
		IRoleStore<RoleEntity>
	{
		private readonly IRoleContext roleContext;

		private bool _disposed;

		public RoleStore(IRoleContext roleContext)
		{
			this.roleContext = roleContext;
		}

		#region Create

		/// <inheritdoc/>
		public Task<IdentityResult> CreateAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		#region Retrieve

		/// <inheritdoc/>
		public Task<RoleEntity> FindByIdAsync(string roleId, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<RoleEntity> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<string> GetNormalizedRoleNameAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<string> GetRoleIdAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<string> GetRoleNameAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		#region Update

		/// <inheritdoc/>
		public Task SetNormalizedRoleNameAsync(RoleEntity role, string normalizedName, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task SetRoleNameAsync(RoleEntity role, string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task<IdentityResult> UpdateAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		#region Delete

		/// <inheritdoc/>
		public Task<IdentityResult> DeleteAsync(RoleEntity role, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_disposed = true;
		}
	}
}

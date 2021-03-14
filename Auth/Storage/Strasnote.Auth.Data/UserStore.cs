// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data
{
	/// <summary>
	/// Handles user accounts and identity CRUD
	/// Implementation reference: https://github.com/aspnet/AspNetIdentity/blob/master/src/Microsoft.AspNet.Identity.EntityFramework/UserStore.cs
	/// </summary>
	public class UserStore :
		IUserEmailStore<UserEntity>,
		IUserPasswordStore<UserEntity>,
		IUserRoleStore<UserEntity>
	{
		private readonly IUserContext userContext;
		private readonly IRoleContext roleContext;

		private bool _disposed;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="userContext">IUserContext</param>
		public UserStore(IUserContext userContext, IRoleContext roleContext) =>
			(this.userContext, this.roleContext) = (userContext, roleContext);

		#region Create

		/// <inheritdoc/>
		public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userContext.CreateAsync(user, cancellationToken);
		}

		#endregion

		#region Retrieve

		/// <inheritdoc/>
		public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userContext.RetrieveAsync(int.Parse(userId), cancellationToken);
		}

		/// <inheritdoc/>
		public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userContext.RetrieveAsync(normalizedUserName, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return Task.FromResult(user.Id.ToString());
		}

		/// <inheritdoc/>
		public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return Task.FromResult(user.NormalizedUserName);
		}

		/// <inheritdoc/>
		public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return Task.FromResult(user.UserName);
		}

		/// <inheritdoc/>
		public Task<string> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			return Task.FromResult(user.Email);
		}

		/// <inheritdoc/>
		public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			return Task.FromResult(user.EmailConfirmed);
		}

		/// <inheritdoc/>
		public async Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await userContext.RetrieveByEmail(normalizedEmail, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return Task.FromResult(user.NormalizedUserName);
		}

		/// <inheritdoc/>
		public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			return Task.FromResult(user.PasswordHash);
		}

		/// <inheritdoc/>
		public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.PasswordHash != null);
		}

		/// <inheritdoc/>
		public async Task<IList<string>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var userRoles = await roleContext.RetrieveForUserAsync(user.Id);

			return userRoles.Select(x => x.Name).ToList();
		}

		/// <inheritdoc/>
		public async Task<bool> IsInRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			if (string.IsNullOrWhiteSpace(roleName))
			{
				throw new ArgumentNullException(nameof(roleName));
			}

			var userRoles = await GetRolesAsync(user, cancellationToken);

			return userRoles.Any(r => r == roleName);
		}

		/// <inheritdoc/>
		public Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		#region Update

		/// <inheritdoc/>
		public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userContext.UpdateAsync(user, cancellationToken);
		}

		/// <inheritdoc/>
		public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			user.NormalizedUserName = normalizedName;

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			user.UserName = userName;

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task SetEmailAsync(UserEntity user, string email, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			user.Email = email;

			return Task.FromResult(0);
		}

		/// <inheritdoc/>
		public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			user.EmailConfirmed = confirmed;

			return Task.FromResult(0);
		}

		/// <inheritdoc/>
		public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			user.NormalizedEmail = normalizedEmail;

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			user.PasswordHash = passwordHash;

			return Task.FromResult(0);
		}

		/// <inheritdoc/>
		public Task AddToRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

		/// <inheritdoc/>
		public Task RemoveFromRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

		#endregion

		#region Delete

		/// <inheritdoc/>
		public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userContext.DeleteAsync(user, cancellationToken);
		}

		#endregion

		/// <inheritdoc/>
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_disposed = true;
		}

		private void ThrowIfDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}
	}
}

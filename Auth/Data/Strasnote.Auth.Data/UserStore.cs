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
		IUserPasswordStore<UserEntity>
	{
		private readonly IUserRepository userRepository;

		private bool _disposed;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="userRepository">IUserRepository</param>
		public UserStore(IUserRepository userRepository) =>
			this.userRepository = userRepository;

		#region Create

		/// <inheritdoc/>
		public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userRepository.CreateAsync<IdentityResult>(user);
		}

		#endregion

		#region Retrieve

		/// <inheritdoc/>
		public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userRepository.RetrieveAsync<UserEntity>(long.Parse(userId));
		}

		/// <inheritdoc/>
		public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userRepository.RetrieveByUsernameAsync<UserEntity>(normalizedUserName);
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
				throw new ArgumentNullException(nameof(user));
			}

			return Task.FromResult(user.Email);
		}

		/// <inheritdoc/>
		public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return Task.FromResult(user.EmailConfirmed);
		}

		/// <inheritdoc/>
		public Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userRepository.RetrieveByEmailAsync<UserEntity>(normalizedEmail);
		}

		/// <inheritdoc/>
		public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return Task.FromResult(user.NormalizedEmail);
		}

		/// <inheritdoc/>
		public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return Task.FromResult(user.PasswordHash);
		}

		/// <inheritdoc/>
		public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.PasswordHash != null);
		}

		#endregion

		#region Update

		/// <inheritdoc/>
		public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return userRepository.UpdateAsync<IdentityResult>(user);
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
				throw new ArgumentNullException(nameof(user));
			}

			user.Email = email;

			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			user.EmailConfirmed = confirmed;

			return Task.CompletedTask;
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
				throw new ArgumentNullException(nameof(user));
			}

			user.PasswordHash = passwordHash;

			return Task.CompletedTask;
		}

		#endregion

		#region Delete

		/// <inheritdoc/>
		public async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await userRepository.DeleteAsync(user.Id).ConfigureAwait(false) switch
			{
				true =>
					IdentityResult.Success,

				false =>
					IdentityResult.Failed()
			};
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

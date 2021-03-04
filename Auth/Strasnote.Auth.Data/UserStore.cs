using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;

namespace Strasnote.Auth.Data
{
	/// <summary>
	/// Handles user accounts and identity CRUD
	/// </summary>
	public class UserStore :
		IUserStore<UserEntity>
	{
		private readonly IUserContext _userContext;

		private bool _disposed;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="userContext">IUserContext</param>
		public UserStore(IUserContext userContext) =>
			_userContext = userContext;

		#region Create

		/// <summary>
		/// Creates the specified <paramref name="user" /> in the user store.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" /> of the creation operation.</returns>
		public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await _userContext.CreateAsync(user, cancellationToken).ConfigureAwait(false);
		}

		#endregion

		#region Retrieve

		/// <summary>
		/// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
		/// </summary>
		/// <param name="userId">The user ID to search for.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
		/// </returns>
		public async Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await _userContext.RetrieveAsync(int.Parse(userId), cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Finds and returns a user, if any, who has the specified normalized user name.
		/// </summary>
		/// <param name="normalizedUserName">The normalized user name to search for.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName" /> if it exists.
		/// </returns>
		public async Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await _userContext.RetrieveAsync(normalizedUserName, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the user identifier for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose identifier should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user" />.</returns>
		public async Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await Task.FromResult(user.Id.ToString()).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the normalized user name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose normalized name should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.</returns>
		public async Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await Task.FromResult(user.NormalizedUserName).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the user name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose name should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.</returns>
		public async Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await Task.FromResult(user.UserName).ConfigureAwait(false);
		}

		#endregion

		#region Update

		/// <summary>
		/// Updates the specified <paramref name="user" /> in the user store.
		/// </summary>
		/// <param name="user">The user to update.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" /> of the update operation.</returns>
		public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await _userContext.UpdateAsync(user, cancellationToken).ConfigureAwait(false);
		}

		public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			user.NormalizedUserName = normalizedName;
			return Task.CompletedTask;
		}

		/// <summary>
		/// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose name should be set.</param>
		/// <param name="userName">The user name to set.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			user.UserName = userName;
			return Task.CompletedTask;
		}

		#endregion

		#region Delete

		/// <summary>
		/// Deletes the specified <paramref name="user" /> from the user store.
		/// </summary>
		/// <param name="user">The user to delete.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" /> of the update operation.</returns>
		public async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return await _userContext.DeleteAsync(user, cancellationToken).ConfigureAwait(false);
		}

		#endregion

		/// <summary>
		/// Dispose user context
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_userContext.Dispose();
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

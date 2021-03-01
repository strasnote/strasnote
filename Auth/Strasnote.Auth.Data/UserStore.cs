using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Abstracts;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Strasnote.Auth.Data
{
	public class UserStore :
		IUserStore<UserEntity>
	{
		private readonly IUserContext _userContext;

		private bool _disposed;

		public UserStore(IUserContext userContext)
		{
			_userContext = userContext;
		}

		public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();

			return _userContext.Retrieve(int.Parse(userId));
		}

		public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
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

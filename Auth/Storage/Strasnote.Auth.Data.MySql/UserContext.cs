// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.MySql
{
	public sealed class UserContext : IUserContext
	{
		public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken) => throw new NotImplementedException();
		public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken) => throw new NotImplementedException();
		public void Dispose() => throw new NotImplementedException();
		public Task<UserEntity> RetrieveAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
		public Task<UserEntity> RetrieveAsync(string name, CancellationToken cancellationToken) => throw new NotImplementedException();
		public Task<UserEntity> RetrieveByEmail(string email, CancellationToken cancellationToken) => throw new NotImplementedException();
		public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken) => throw new NotImplementedException();
	}
}
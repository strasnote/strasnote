// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Fake;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IRefreshTokenContext"/>
	public sealed class RefreshTokenContext : DbContext<RefreshTokenEntity>, IRefreshTokenContext
	{
		public RefreshTokenContext(ILog<RefreshTokenContext> log) : base(log) { }

		public Task CreateAsync(RefreshTokenEntity entity)
		{
			LogOperation(Operation.Create, "{Token}", entity);
			return Task.CompletedTask;
		}

		public Task<RefreshTokenEntity> RetrieveForUserAsync(long userId, string refreshToken)
		{
			LogOperation("RetrieveForUser", "{UserId} {Token}", userId, refreshToken);
			return Task.FromResult(new RefreshTokenEntity("token", DateTimeOffset.Now.AddDays(1), 1));
		}

		public Task<bool> DeleteByUserIdAsync(long userId)
		{
			LogOperation(Operation.Delete, "{UserId}", userId);
			return Task.FromResult(true);
		}

		protected override object GetFakeModelForCreate() => throw new NotImplementedException();
		protected override object GetFakeModelForRetrieve() => throw new NotImplementedException();
		protected override object GetFakeModelForRetrieveById(long id) => throw new NotImplementedException();
		protected override object GetFakeModelForUpdate() => throw new NotImplementedException();
		public Task<System.Collections.Generic.IEnumerable<TModel>> QueryAsync<TModel>(params (Expression<Func<RefreshTokenEntity, object>> property, SearchOperator op, object value)[] predicates) => throw new NotImplementedException();
		public Task<TModel> QuerySingleAsync<TModel>(params (Expression<Func<RefreshTokenEntity, object>> property, SearchOperator op, object value)[] predicates) => throw new NotImplementedException();
	}
}

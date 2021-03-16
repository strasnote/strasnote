// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Fake;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IRoleContext"/>
	public sealed class RoleContext : DbContext<RoleEntity>, IRoleContext
	{
		public RoleContext(ILog<RoleContext> log) : base(log) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveByNameAsync<TModel>(string roleName)
		{
			LogOperation("RetrieveByName", "{RoleName}", roleName);
			return ConvertToModel<TModel>(new RoleEntity { Name = roleName });
		}

		/// <inheritdoc/>
		public Task<IEnumerable<TModel>> RetrieveForUserAsync<TModel>(long userId)
		{
			LogOperation("RetrieveForUser", "{UserId}", userId);

			List<RoleEntity> roles = new()
			{
				new RoleEntity
				{
					Id = userId,
					Name = "TestRole"
				}
			};

			return ConvertToModel<IEnumerable<TModel>>(roles.AsEnumerable());
		}

		protected override object GetFakeModelForCreate() => throw new System.NotImplementedException();
		protected override object GetFakeModelForRetrieve() => throw new System.NotImplementedException();
		protected override object GetFakeModelForRetrieveById(long id) => throw new System.NotImplementedException();
		protected override object GetFakeModelForUpdate() => throw new System.NotImplementedException();
	}
}

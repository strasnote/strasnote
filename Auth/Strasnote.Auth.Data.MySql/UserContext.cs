using System;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Data.Entities;

namespace Strasnote.Auth.Data.MySql
{
	public sealed class UserContext : IUserContext
	{
		public Task<UserEntity> Retrieve(int id)
		{
			throw new NotImplementedException();
		}
	}
}

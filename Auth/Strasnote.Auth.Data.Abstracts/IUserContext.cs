using Strasnote.Auth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strasnote.Auth.Data.Abstracts
{
	public interface IUserContext
	{
		Task<UserEntity> Retrieve(int id);
	}
}

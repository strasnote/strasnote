using System.Threading.Tasks;
using Strasnote.Auth.Data.Entities;

namespace Strasnote.Auth.Data.Abstracts
{
	public interface IUserContext
	{
		Task<UserEntity> Retrieve(int id);
	}
}

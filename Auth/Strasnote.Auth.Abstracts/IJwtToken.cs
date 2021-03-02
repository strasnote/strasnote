using System.Threading.Tasks;
using Strasnote.Auth.Models;

namespace Strasnote.Auth.Abstracts
{
	public interface IJwtToken
	{
		Task<TokenResponse> GetToken();

		Task<TokenResponse> GetRefreshToken();
	}
}

using System.Threading.Tasks;
using Strasnote.Auth.Models;

namespace Strasnote.Auth.Abstracts
{
	/// <summary>
	/// JSON Web Token
	/// </summary>
	public interface IJwtToken
	{
		/// <summary>
		/// Get token content
		/// </summary>
		Task<TokenResponse> GetTokenAsync();

		/// <summary>
		/// Get refresh token
		/// </summary>
		Task<TokenResponse> GetRefreshTokenAsync();
	}
}

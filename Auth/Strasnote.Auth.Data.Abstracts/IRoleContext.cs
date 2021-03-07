using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Entities;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the user role database.
	/// </summary>
	public interface IRoleContext
	{
		/// <summary>
		/// Retrieve a role by <paramref name="roleName"/>.
		/// </summary>
		/// <param name="roleName">Role name.</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task<RoleEntity> RetrieveAsync(string roleName, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieve the roles for a user, by <paramref name="userId"/>.
		/// </summary>
		/// <param name="userId">User ID.</param>
		Task<IList<RoleEntity>> RetrieveForUserAsync(long userId);
	}
}

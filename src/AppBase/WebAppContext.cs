// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.AppBase
{
	/// <inheritdoc cref="IAppContext"/>
	public sealed class WebAppContext : IAppContext
	{
		/// <inheritdoc/>
		public bool IsAuthenticated { get; init; }

		/// <inheritdoc/>
		public ulong? CurrentUserId { get; init; }

		/// <summary>
		/// Set context information
		/// </summary>
		/// <param name="context">IHttpContextAccessor</param>
		public WebAppContext(IHttpContextAccessor context)
		{
			if (context.HttpContext?.User is ClaimsPrincipal principal)
			{
				CurrentUserId = GetCurrentUserId(principal.Claims);
				IsAuthenticated = GetUserIsAuthenticated(CurrentUserId, principal.Identity);
			}
		}

		/// <summary>
		/// Get Current User ID from Claims
		/// </summary>
		/// <param name="claims">List of Claims</param>
		static internal ulong? GetCurrentUserId(IEnumerable<Claim> claims) =>
			claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier) switch
			{
				Claim idClaim =>
					ulong.TryParse(idClaim.Value, out ulong id) switch
					{
						true =>
							id,

						false =>
							null
					},

				_ =>
					null
			};

		/// <summary>
		/// If the User ID is set and identity is authenticated, returns true
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="identity">IIdentity</param>
		static internal bool GetUserIsAuthenticated(ulong? userId, IIdentity? identity) =>
			userId is ulong && (identity?.IsAuthenticated ?? false);
	}
}

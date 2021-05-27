// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.AppBase
{
	/// <inheritdoc cref="IAppContext"/>
	public sealed class AppContext : IAppContext
	{
		/// <inheritdoc/>
		public bool IsAuthenticated { get; init; }

		/// <inheritdoc/>
		public long? CurrentUserId { get; init; }

		/// <summary>
		/// Set context information
		/// </summary>
		/// <param name="context">IHttpContextAccessor</param>
		public AppContext(IHttpContextAccessor context)
		{
			if (context.HttpContext?.User is ClaimsPrincipal principal)
			{
				CurrentUserId = GetCurrentUserId(principal.Claims);
				IsAuthenticated = CurrentUserId is long && (principal.Identity?.IsAuthenticated ?? false);
			}
		}

		/// <summary>
		/// Get Current User ID from Claims
		/// </summary>
		/// <param name="claims">List of Claims</param>
		static internal long? GetCurrentUserId(IEnumerable<Claim> claims) =>
			claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier) switch
			{
				Claim idClaim =>
					long.TryParse(idClaim.Value, out long id) switch
					{
						true =>
							id,

						false =>
							null
					},

				_ =>
					null
			};
	}
}

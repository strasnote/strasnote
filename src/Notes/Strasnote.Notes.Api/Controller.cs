// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;

namespace Strasnote.Notes.Api
{
	/// <summary>
	/// Controller Base
	/// </summary>
	public abstract class Controller : Microsoft.AspNetCore.Mvc.Controller
	{
		/// <summary>
		/// IAppContext
		/// </summary>
		protected IAppContext Context { get; private init; }

		/// <summary>
		/// ILog
		/// </summary>
		protected ILog Log { get; private init; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="context">IAppContext</param>
		/// <param name="log">ILog</param>
		protected Controller(IAppContext context, ILog log) =>
			(Context, Log) = (context, log);

		/// <summary>
		/// Run standard authentication checks
		/// </summary>
		/// <typeparam name="T">Return model type</typeparam>
		/// <param name="then">Runs if current user is authenticated</param>
		/// <param name="result">[Optional] If set, will return this IActionResult instead of JSON model</param>
		/// <param name="otherwise">[Optional] If set, will return if user is not authenticated, instead of Unauthorized</param>
		protected Task<IActionResult> IsAuthenticatedUserAsync<T>(
			Func<long, Task<T>> then,
			Func<T, IActionResult>? result = null,
			Func<IActionResult>? otherwise = null
		)
		{
			return Is.AuthenticatedUser(
				ctx: Context,

				//
				// Runs if the request has a valid authenticated user
				//

				then: async userId =>
				{
					try
					{
						// Execute 'then' to get value
						var value = await then(userId).ConfigureAwait(false);

						// Process the value
						return value switch
						{
							// Return value using custom result function
							T when result is not null =>
								result(value),

							// Return value as JSON
							T =>
								Json(value),

							// Value is null so return not found
							_ =>
								NotFound()
						};
					}
					catch (Exception ex)
					{
						// Log any errors that occurred while function was being executed
						Log.Error(ex, "Error processing controller function.");
						return StatusCode(500);
					}
				},

				//
				// Runs if the request is not authorised
				//

				otherwise: () =>
				{
					// Return unauthorised by default
					if (otherwise is null)
					{
						return Unauthorized();
					}

					try
					{
						// Execute function
						return otherwise();
					}
					catch (Exception ex)
					{
						// Log any errors that occurred while function was being executed
						Log.Error(ex, "Error processing unauthorised function.");
						return StatusCode(500);
					}
				}
			);
		}
	}
}

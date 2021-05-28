// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.Util
{
	/// <summary>
	/// Common functions to run based on conditions
	/// </summary>
	public static class Is
	{
		/// <summary>
		/// Run if the user defined in <paramref name="ctx"/> is correctly authenticated
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="ctx">IAppContext</param>
		/// <param name="then">Runs if user is correctly authenticated, passing the User ID as a parameter</param>
		/// <param name="otherwise">Runs if user is not authenticated</param>
		public static Task<T> AuthenticatedUser<T>(IAppContext ctx, Func<long, Task<T>> then, Func<T> otherwise)
		{
			if (ctx.IsAuthenticated && ctx.CurrentUserId is long userId)
			{
				return then(userId);
			}
			else
			{
				return Task.FromResult(otherwise());
			}
		}
	}
}

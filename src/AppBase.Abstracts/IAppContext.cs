// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.AppBase.Abstracts
{
	/// <summary>
	/// Contains data about the app running context
	/// </summary>
	public interface IAppContext
	{
		/// <summary>
		/// Whether or not the current request is authenticated
		/// </summary>
		bool IsAuthenticated { get; }

		/// <summary>
		/// Current User ID
		/// </summary>
		long? CurrentUserId { get; }
	}
}

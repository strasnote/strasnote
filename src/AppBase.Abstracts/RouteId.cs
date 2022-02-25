// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.AppBase.Abstracts
{
	/// <summary>
	/// Used to wrap IDs in URL routes, to enable Fluent Validation.
	/// </summary>
	/// <param name="Value">Id Value</param>
	public abstract record class RouteId(ulong Value)
	{
		/// <summary>
		/// Define a parameterless constructor to suppor MVC model binding
		/// </summary>
		protected RouteId() : this(0UL) { }
	}
}

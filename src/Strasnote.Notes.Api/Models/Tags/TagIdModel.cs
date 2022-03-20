// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.AppBase.Abstracts;

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// Tag ID (wrapped for fluent validation)
	/// </summary>
	public sealed record class TagIdModel : RouteId;
}

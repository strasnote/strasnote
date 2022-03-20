// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.AppBase.Abstracts;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// Folder ID (wrapped for fluent validation)
	/// </summary>
	public sealed record class FolderIdModel : RouteId;
}

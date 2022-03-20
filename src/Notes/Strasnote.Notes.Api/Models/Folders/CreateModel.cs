// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Controllers.FolderController.Create(CreateModel)"/>
	/// </summary>
	/// <param name="FolderName">New folder name</param>
	public sealed record CreateModel(string FolderName);
}

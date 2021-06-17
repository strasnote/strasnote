// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Folders;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.FolderController.SaveName(long, SaveNameModel)"/>
	/// </summary>
	/// <param name="FolderName">Folder Name</param>
	public sealed record SaveNameModel(string FolderName);
}

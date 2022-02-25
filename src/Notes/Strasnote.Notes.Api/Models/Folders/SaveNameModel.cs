// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Controllers.FolderController.SaveName(FolderIdModel, SaveNameModel)"/>
	/// </summary>
	/// <param name="FolderName">Folder Name</param>
	public sealed record SaveNameModel(string FolderName);
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Folders;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.FolderController.CreateInFolder(CreateInFolderModel)"/>
	/// </summary>
	/// <param name="FolderName">New folder name</param>
	/// <param name="ParentId">ID of parent folder</param>
	public sealed record CreateInFolderModel(string FolderName, ulong ParentId);
}

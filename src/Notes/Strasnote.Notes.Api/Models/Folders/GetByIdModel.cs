// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.FolderController.GetById(long)"/>
	/// </summary>
	/// <param name="Id">Folder ID</param>
	/// <param name="FolderName">Folder name</param>
	/// <param name="FolderCreated">When the folder was created</param>
	/// <param name="FolderUpdated">When the folder was last updated</param>
	public sealed record GetByIdModel(long Id, string? FolderName, DateTime FolderCreated, DateTime FolderUpdated);
}

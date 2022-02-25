// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Controllers.FolderController.GetById(FolderIdModel)"/>
	/// </summary>
	/// <param name="FolderCreated">When the folder was created</param>
	/// <param name="FolderName">Folder name</param>
	/// <param name="FolderUpdated">When the folder was last updated</param>
	/// <param name="Id">Folder ID</param>
	public sealed record GetByIdModel(DateTime FolderCreated, string FolderName, DateTime FolderUpdated, ulong Id);
}

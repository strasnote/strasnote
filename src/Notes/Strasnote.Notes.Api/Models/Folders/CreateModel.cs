﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Folders;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.FolderController.Create(CreateModel)"/>
	/// </summary>
	/// <param name="FolderName">New folder name</param>
	public sealed record CreateModel(string FolderName);
}

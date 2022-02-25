// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.CreateInFolder(CreateInFolderModel)"/>
	/// </summary>
	/// <param name="FolderId">ID of parent folder</param>
	public sealed record CreateInFolderModel(ulong FolderId);
}

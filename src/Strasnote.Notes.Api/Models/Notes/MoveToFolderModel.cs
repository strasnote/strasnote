// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.MoveToFolder(NoteIdModel, MoveToFolderModel)"/>
	/// </summary>
	public sealed record MoveToFolderModel
	{
		/// <summary>
		/// See <see cref="Controllers.NoteController.MoveToFolder(NoteIdModel, MoveToFolderModel)"/>
		/// </summary>
		public ulong? FolderId { get; init; }
	}
}

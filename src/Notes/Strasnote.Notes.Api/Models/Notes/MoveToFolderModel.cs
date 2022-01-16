// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.MoveToFolder(ulong, MoveToFolderModel)"/>
	/// </summary>
	public sealed record MoveToFolderModel
	{
		/// <summary>
		/// See <see cref="Controllers.NoteController.MoveToFolder(ulong, MoveToFolderModel)"/>
		/// </summary>
		public ulong? FolderId { get; init; }
	}
}

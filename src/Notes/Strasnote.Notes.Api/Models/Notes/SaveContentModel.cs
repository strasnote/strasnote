// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.SaveContent(NoteIdModel, SaveContentModel)"/>
	/// </summary>
	/// <param name="NoteContent">Note content</param>
	public sealed record SaveContentModel(string NoteContent);
}

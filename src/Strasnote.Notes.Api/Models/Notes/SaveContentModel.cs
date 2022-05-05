// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.SaveContent(NoteIdModel, SaveContentModel)"/>
	/// Note: we don't care if somebody saves a blank note, hence no validation here.
	/// </summary>
	/// <param name="NoteContent">Note content</param>
	public sealed record SaveContentModel(string NoteContent);
}

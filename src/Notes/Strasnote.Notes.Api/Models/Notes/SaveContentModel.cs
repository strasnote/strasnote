// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.NoteController.SaveContent(long, SaveContentModel)"/>
	/// </summary>
	/// <param name="NoteContent">Note content</param>
	public sealed record SaveContentModel(string NoteContent);
}

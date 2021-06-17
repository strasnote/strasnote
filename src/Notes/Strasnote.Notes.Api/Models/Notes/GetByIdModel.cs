// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.NoteController.GetById(long)"/>
	/// </summary>
	/// <param name="Id">Note ID</param>
	/// <param name="NoteContent">Note content</param>
	/// <param name="NoteCreated">When the note was created</param>
	/// <param name="NoteUpdated">When the note was last updated</param>
	public sealed record GetByIdModel(long Id, string? NoteContent, DateTime NoteCreated, DateTime NoteUpdated);
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.NoteController.AddTag(ulong, AddTagModel)"/>
	/// </summary>
	/// <param name="TagId">Tag ID</param>
	public sealed record AddTagModel(ulong TagId);
}

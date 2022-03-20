// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.AddTag(NoteIdModel, AddTagModel)"/>
	/// </summary>
	/// <param name="TagId">Tag ID</param>
	public sealed record AddTagModel(ulong TagId);
}

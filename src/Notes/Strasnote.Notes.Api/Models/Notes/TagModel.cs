// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.NoteController.GetTags(ulong)"/>
	/// </summary>
	/// <param name="Id">Tag ID</param>
	/// <param name="TagName">Tag name</param>
	/// <param name="TagNameNormalised">Tag name (normalised)</param>
	public sealed record TagModel(ulong Id, string TagName, string TagNameNormalised);
}

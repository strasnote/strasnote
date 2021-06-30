// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.TagController.SaveContent(ulong, SaveNameModel)"/>
	/// </summary>
	/// <param name="TagName">Tag name</param>
	public sealed record SaveNameModel(string TagName);
}

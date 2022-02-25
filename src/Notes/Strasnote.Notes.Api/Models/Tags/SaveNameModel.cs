// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// See <see cref="Controllers.TagController.SaveName(TagIdModel, SaveNameModel)"/>
	/// </summary>
	/// <param name="TagName">Tag name</param>
	public sealed record SaveNameModel(string TagName);
}

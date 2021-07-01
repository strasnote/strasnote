// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// See <see cref="Strasnote.Notes.Api.Controllers.TagController.GetById(ulong)"/>
	/// </summary>
	/// <param name="Id">Note ID</param>
	/// <param name="TagCreated">When the tag was created</param>
	/// <param name="TagName">Tag name</param>
	/// <param name="TagNameNormalised">Tag name (with additional characters stripped out)</param>
	/// <param name="TagUpdated">When the tag was last updated</param>
	public sealed record GetByIdModel(ulong Id, DateTime TagCreated, string TagName, string TagNameNormalised, DateTime TagUpdated);
}

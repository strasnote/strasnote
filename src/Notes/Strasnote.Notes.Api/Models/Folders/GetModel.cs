// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Folders
{
	public sealed class GetModel
	{
		public long Id { get; init; }

		public string FolderName { get; init; } = string.Empty;

		public DateTime FolderCreated { get; init; }

		public DateTime FolderUpdated { get; init; }
	}
}

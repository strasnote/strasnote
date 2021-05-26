// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Api.Models.Folders
{
	public record CreateModel
	{
		public long? FolderParentId { get; init; }

		public string FolderName { get; set; } = string.Empty;
	}
}

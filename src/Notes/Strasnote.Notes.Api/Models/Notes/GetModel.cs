// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Notes.Api.Models.Notes
{
	public sealed class GetModel
	{
		public long Id { get; init; }

		public string NoteContent { get; init; } = string.Empty;

		public DateTime NoteCreated { get; init; }

		public DateTime NoteUpdated { get; init; }
	}
}

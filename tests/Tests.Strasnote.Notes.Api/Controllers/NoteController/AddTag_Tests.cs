// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Api.Models.Tags;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class AddTag_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Tags_AddToNote()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = new NoteIdModel { Value = Rnd.Ulng };
			var tagId = new TagIdModel { Value = Rnd.Ulng };

			// Act
			await controller.AddTag(noteId, new(tagId.Value));

			// Assert
			await v.Tags.Received().AddToNote(tagId.Value, noteId.Value, v.UserId);
		}
	}
}

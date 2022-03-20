// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class MoveToFolderTests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_UpdateAsync_With_FolderId_Object()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = new NoteIdModel { Value = Rnd.Ulng };
			var folderId = Rnd.Ulng;

			var model = new MoveToFolderModel
			{
				FolderId = folderId
			};

			// Act
			await controller.MoveToFolder(noteId, model);

			// Assert
			await v.Notes.Received().UpdateAsync(noteId.Value, model, v.UserId);
		}
	}
}

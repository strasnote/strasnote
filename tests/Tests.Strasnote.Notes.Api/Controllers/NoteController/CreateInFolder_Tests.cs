// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Folders;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class CreateInFolder_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Notes_CreateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderIdModel = new FolderIdModel
			{
				Value = Rnd.Ulng
			};

			// Act
			await controller.CreateInFolder(folderIdModel);

			// Assert
			await v.Notes.Received().CreateAsync(Arg.Is<NoteEntity>(n => n.UserId == v.UserId && n.FolderId == folderIdModel.Value));
		}
	}
}

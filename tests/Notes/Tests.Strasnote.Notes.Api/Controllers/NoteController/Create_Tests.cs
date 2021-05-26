// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Create_Tests
	{
		[Fact]
		public async Task Calls_Notes_CreateAsync()
		{
			// Arrange
			var notes = Substitute.For<INoteRepository>();
			var controller = new NoteController(notes);

			// Act
			await controller.Create(new CreateModel(1)).ConfigureAwait(false);

			// Assert
			await notes.Received().CreateAsync(Arg.Any<NoteEntity>()).ConfigureAwait(false);
		}

		[Fact]
		public async Task Sets_FolderId_On_NoteEntity()
		{
			// Arrange
			var notes = Substitute.For<INoteRepository>();
			var controller = new NoteController(notes);
			var createNoteModel = new CreateModel(1);

			// Act
			await controller.Create(createNoteModel).ConfigureAwait(false);

			// Assert
			await notes.Received().CreateAsync(new NoteEntity { FolderId = createNoteModel.FolderId }).ConfigureAwait(false);
		}
	}
}

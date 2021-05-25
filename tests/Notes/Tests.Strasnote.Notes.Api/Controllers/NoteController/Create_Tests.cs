// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
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
			var note = new NoteEntity();

			// Act
			await controller.Create(note).ConfigureAwait(false);

			// Assert
			await notes.Received().CreateAsync(note).ConfigureAwait(false);
		}
	}
}

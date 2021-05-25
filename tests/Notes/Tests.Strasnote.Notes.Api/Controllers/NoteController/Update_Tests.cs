// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Update_Tests
	{
		[Fact]
		public async Task Calls_Notes_UpdateAsync()
		{
			// Arrange
			var notes = Substitute.For<INoteRepository>();
			var controller = new NoteController(notes);
			var id = Rnd.Lng;
			var model = new UpdateModel();

			// Act
			await controller.Update(id, model).ConfigureAwait(false);

			// Assert
			await notes.Received().UpdateAsync(id, model).ConfigureAwait(false);
		}
	}
}

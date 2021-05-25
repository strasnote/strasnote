// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Delete_Tests
	{
		[Fact]
		public async Task Calls_Notes_DeleteAsync()
		{
			// Arrange
			var notes = Substitute.For<INoteRepository>();
			var controller = new NoteController(notes);
			var id = Rnd.Lng;

			// Act
			await controller.Delete(id).ConfigureAwait(false);

			// Assert
			await notes.Received().DeleteAsync(id).ConfigureAwait(false);
		}
	}
}

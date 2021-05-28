// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Delete_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Notes_DeleteAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = Rnd.Lng;

			// Act
			await controller.Delete(noteId).ConfigureAwait(false);

			// Assert
			await v.Notes.Received().DeleteAsync(noteId, v.UserId).ConfigureAwait(false);
		}
	}
}

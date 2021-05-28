// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Update_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Notes_UpdateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = Rnd.Lng;
			var model = new UpdateModel();

			// Act
			await controller.Update(noteId, model);

			// Assert
			await v.Notes.Received().UpdateAsync(noteId, model, v.UserId);
		}
	}
}

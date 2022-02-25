// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Notes;
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
			var noteId = new NoteIdModel { Value = Rnd.Ulng };

			// Act
			await controller.Delete(noteId);

			// Assert
			await v.Notes.Received().DeleteAsync(noteId.Value, v.UserId);
		}
	}
}

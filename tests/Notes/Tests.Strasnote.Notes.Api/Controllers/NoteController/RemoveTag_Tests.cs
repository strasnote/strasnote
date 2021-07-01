// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class RemoveTag_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Tags_RemoveFromNote()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = Rnd.Ulng;
			var tagId = Rnd.Ulng;

			// Act
			await controller.RemoveTag(noteId, tagId);

			// Assert
			await v.Tags.Received().RemoveFromNote(tagId, noteId, v.UserId);
		}
	}
}

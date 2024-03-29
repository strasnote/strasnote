﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Notes.Api.Models.Notes;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class GetById_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Notes_RetrieveAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var noteId = new NoteIdModel { Value = Rnd.Ulng };

			// Act
			await controller.GetById(noteId);

			// Assert
			await v.Notes.Received().RetrieveAsync<GetByIdModel?>(noteId.Value, v.UserId);
		}
	}
}

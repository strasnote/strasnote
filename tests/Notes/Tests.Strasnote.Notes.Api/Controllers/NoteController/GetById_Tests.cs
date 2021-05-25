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
	public class GetById_Tests
	{
		[Fact]
		public async Task Calls_Notes_RetrieveAsync()
		{
			// Arrange
			var notes = Substitute.For<INoteRepository>();
			var controller = new NoteController(notes);
			var id = Rnd.Lng;

			// Act
			await controller.GetById(id).ConfigureAwait(false);

			// Assert
			await notes.Received().RetrieveAsync<GetModel?>(id).ConfigureAwait(false);
		}
	}
}

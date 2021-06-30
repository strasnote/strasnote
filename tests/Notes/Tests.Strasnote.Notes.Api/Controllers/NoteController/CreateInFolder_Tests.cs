// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class CreateInFolder_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Calls_Notes_CreateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderId = Rnd.Ulng;

			// Act
			await controller.CreateInFolder(new(folderId));

			// Assert
			await v.Notes.Received().CreateAsync(Arg.Is<NoteEntity>(n => n.UserId == v.UserId && n.FolderId == folderId));
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public class Create_Tests : NoteController_Tests
	{
		[Fact]
		public async Task Without_FolderId_Calls_Notes_CreateAsync()
		{
			// Arrange
			var (controller, v) = Setup();

			// Act
			await controller.Create().ConfigureAwait(false);

			// Assert
			await v.Notes.Received().CreateAsync(v.UserId).ConfigureAwait(false);
		}

		[Fact]
		public async Task With_FolderId_Calls_Notes_CreateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderId = Rnd.Lng;

			// Act
			await controller.Create(folderId).ConfigureAwait(false);

			// Assert
			await v.Notes.Received().CreateAsync(v.UserId, folderId).ConfigureAwait(false);
		}
	}
}

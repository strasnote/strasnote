// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Update_Tests
	{
		[Fact]
		public async Task Calls_Folders_UpdateAsync()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var id = Rnd.Lng;
			var model = new UpdateModel();

			// Act
			await controller.Update(id, model).ConfigureAwait(false);

			// Assert
			await folders.Received().UpdateAsync(id, model).ConfigureAwait(false);
		}
	}
}

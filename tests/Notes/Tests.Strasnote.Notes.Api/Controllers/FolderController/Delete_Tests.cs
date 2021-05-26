// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Delete_Tests
	{
		[Fact]
		public async Task Calls_Folders_DeleteAsync()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var id = Rnd.Lng;

			// Act
			await controller.Delete(id).ConfigureAwait(false);

			// Assert
			await folders.Received().DeleteAsync(id).ConfigureAwait(false);
		}
	}
}

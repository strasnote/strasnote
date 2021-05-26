// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Data.Abstracts;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Create_Tests
	{
		[Fact]
		public async Task Calls_Folders_CreateAsync()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var folder = new FolderEntity();

			// Act
			await controller.Create(folder).ConfigureAwait(false);

			// Assert
			await folders.Received().CreateAsync(folder).ConfigureAwait(false);
		}
	}
}

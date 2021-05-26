// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Folders;
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

			// Act
			await controller.Create(new CreateModel("test")).ConfigureAwait(false);

			// Assert
			await folders.Received().CreateAsync(Arg.Any<FolderEntity>()).ConfigureAwait(false);
		}

		[Fact]
		public async Task Sets_FolderParentId_On_FolderEntity()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var createFolderModel = new CreateModel("test")
			{
				FolderParentId = 1
			};

			// Act
			await controller.Create(createFolderModel).ConfigureAwait(false);

			// Assert
			await folders.Received().CreateAsync(new FolderEntity
			{
				FolderName = createFolderModel.FolderName,
				FolderParentId = createFolderModel.FolderParentId
			}).ConfigureAwait(false);
		}

		[Fact]
		public async Task Sets_FolderParentId_To_Null_On_FolderEntity()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var createFolderModel = new CreateModel("test");

			// Act
			await controller.Create(createFolderModel).ConfigureAwait(false);

			// Assert
			await folders.Received().CreateAsync(new FolderEntity
			{
				FolderName = createFolderModel.FolderName,
				FolderParentId = null
			}).ConfigureAwait(false);
		}
	}
}

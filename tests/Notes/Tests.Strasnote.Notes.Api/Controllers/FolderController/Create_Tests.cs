// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Create_Tests : FolderController_Tests
	{
		[Fact]
		public async Task Calls_Folders_CreateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderName = Rnd.Str;

			// Act
			await controller.Create(new(folderName));

			// Assert
			await v.Folders.Received().CreateAsync(
				Arg.Is<FolderEntity>(f => f.FolderName == folderName)
			);
		}

		[Fact]
		public async Task Sets_FolderParentId_On_FolderEntity()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderName = Rnd.Str;
			var folderParentId = Rnd.Ulng;

			// Act
			await controller.CreateInFolder(new(folderName, folderParentId));

			// Assert
			await v.Folders.Received().CreateAsync(
				Arg.Is<FolderEntity>(f => f.FolderName == folderName && f.FolderParentId == folderParentId)
			);
		}

		[Fact]
		public async Task Sets_FolderParentId_To_Null_On_FolderEntity()
		{
			// Arrange
			var (controller, v) = Setup();
			var folderName = Rnd.Str;

			// Act
			await controller.Create(new(folderName));

			// Assert
			await v.Folders.Received().CreateAsync(
				Arg.Is<FolderEntity>(f => f.FolderName == folderName && f.FolderParentId == null)
			);
		}
	}
}

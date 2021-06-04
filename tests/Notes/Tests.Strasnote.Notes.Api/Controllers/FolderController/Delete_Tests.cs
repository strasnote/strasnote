// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Delete_Tests : FolderController_Tests
	{
		[Fact]
		public async Task Calls_Folders_DeleteAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var id = Rnd.Lng;

			// Act
			await controller.Delete(id);

			// Assert
			await v.Folders.Received().DeleteAsync(id, v.UserId);
		}
	}
}

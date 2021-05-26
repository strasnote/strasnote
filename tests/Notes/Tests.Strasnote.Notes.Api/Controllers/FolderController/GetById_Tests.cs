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
	public class GetById_Tests
	{
		[Fact]
		public async Task Calls_Folders_RetrieveAsync()
		{
			// Arrange
			var folders = Substitute.For<IFolderRepository>();
			var controller = new FolderController(folders);
			var id = Rnd.Lng;

			// Act
			await controller.GetById(id).ConfigureAwait(false);

			// Assert
			await folders.Received().RetrieveAsync<GetModel?>(id).ConfigureAwait(false);
		}
	}
}

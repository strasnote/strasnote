﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class Update_Tests : FolderController_Tests
	{
		[Fact]
		public async Task Calls_Folders_UpdateAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var id = Rnd.Lng;
			var model = new UpdateModel();

			// Act
			await controller.Update(id, model).ConfigureAwait(false);

			// Assert
			await v.Folders.Received().UpdateAsync(id, model, v.UserId).ConfigureAwait(false);
		}
	}
}

﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public class GetById_Tests : FolderController_Tests
	{
		[Fact]
		public async Task Calls_Folders_RetrieveAsync()
		{
			// Arrange
			var (controller, v) = Setup();
			var id = Rnd.Lng;

			// Act
			await controller.GetById(id).ConfigureAwait(false);

			// Assert
			await v.Folders.Received().RetrieveAsync<GetModel?>(id, v.UserId).ConfigureAwait(false);
		}
	}
}

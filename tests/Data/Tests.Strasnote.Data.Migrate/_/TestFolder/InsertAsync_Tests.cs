// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Migrate.TestFolder_Tests
{
	public class InsertAsync_Tests
	{
		[Fact]
		public async Task Calls_Repo_CreateAsync_With_New_Folder_Entity()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var repo = Substitute.For<IFolderRepository>();
			var userId = Rnd.Ulng;

			// Act
			await TestFolder.InsertAsync(log, repo, userId);

			// Assert
			await repo.Received().CreateAsync(Arg.Is<FolderEntity>(f => f.UserId == userId));
		}
	}
}

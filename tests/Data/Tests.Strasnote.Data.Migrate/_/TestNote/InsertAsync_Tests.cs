// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Migrate.TestNote_Tests
{
	public class InsertAsync_Tests
	{
		[Fact]
		public async Task Calls_Repo_CreateAsync_With_New_Note_Entity()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var repo = Substitute.For<INoteRepository>();
			var userId = Rnd.Ulng;
			var folderId = Rnd.Ulng;

			// Act
			await TestNote.InsertAsync(log, repo, userId, folderId);

			// Assert
			await repo.Received().CreateAsync(Arg.Is<NoteEntity>(n => n.UserId == userId && n.FolderId == folderId));
		}
	}
}

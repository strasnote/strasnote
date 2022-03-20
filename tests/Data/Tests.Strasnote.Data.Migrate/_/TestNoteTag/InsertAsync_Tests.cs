// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Entities.Notes;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrate.TestNoteTag_Tests
{
	public class InsertAsync_Tests
	{
		[Fact]
		public async Task Calls_Repo_CreateAsync_With_New_NoteTag_Entity()
		{
			// Arrange
			var log = Substitute.For<ILog>();
			var repo = Substitute.For<INoteTagRepository>();
			var noteId = Rnd.Ulng;
			var tagId = Rnd.Ulng;

			// Act
			await TestNoteTag.InsertAsync(log, repo, noteId, tagId);

			// Assert
			await repo.Received().CreateAsync(Arg.Is<NoteTagEntity>(n => n.NoteId == noteId && n.TagId == tagId));
		}
	}
}

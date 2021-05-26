// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(5, "Update main.note.NoteContent to be nullable")]
	public sealed class Nullable_NoteContent_0005 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				ALTER TABLE `main.note`
				CHANGE COLUMN `NoteContent` `NoteContent` TEXT NULL DEFAULT NULL 
				COLLATE 'utf8_general_ci' AFTER `Id`;
			");
		}

		protected override void Down()
		{
			Execute(@"
				ALTER TABLE `main.note`
				CHANGE COLUMN `NoteContent` `NoteContent` TEXT NOT NULL 
				COLLATE 'utf8_general_ci' AFTER `Id`
			");
		}
	}
}

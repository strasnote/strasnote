// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(7, "main.note.FolderId is now nullable (null = unfiled)")]
	public sealed class Note_FolderId_Nullable : Migration
	{
		protected override void Up() => Execute(@"
			ALTER TABLE `main.note`
			CHANGE COLUMN `FolderId` `FolderId` BIGINT(20) NULL DEFAULT NULL AFTER `NoteUpdated`;
		");

		protected override void Down() => Execute(@"
			ALTER TABLE `main.note`
			CHANGE COLUMN `FolderId` `FolderId` BIGINT(20) NOT NULL DEFAULT '0' AFTER `NoteUpdated`;
		");
	}
}

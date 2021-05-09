// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(2, "Create tables: main.note")]
	public sealed class CreateTable_Note_0002 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `main.note` (
					`NoteId` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`NoteContent` TEXT NOT NULL DEFAULT '' COLLATE 'utf8_general_ci',
					`NoteCreated` DATETIME NOT NULL DEFAULT current_timestamp(),
					`NoteUpdated` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
					`FolderId` BIGINT(20) NOT NULL DEFAULT '0',
					`UserId` BIGINT(20) NOT NULL DEFAULT '0',
					PRIMARY KEY (`NoteId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE `main.note`;");
		}
	}
}

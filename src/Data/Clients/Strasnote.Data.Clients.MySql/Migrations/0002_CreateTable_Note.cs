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
					`Id` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`NoteContent` TEXT NOT NULL DEFAULT '' COLLATE 'utf8_general_ci',
					`NoteCreated` TIMESTAMP NOT NULL DEFAULT current_timestamp(),
					`NoteUpdated` TIMESTAMP NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
					`FolderId` BIGINT(20) NOT NULL DEFAULT '0',
					`UserId` BIGINT(20) NOT NULL DEFAULT '0',
					PRIMARY KEY (`Id`) USING BTREE,
					INDEX `FolderId` (`FolderId`) USING BTREE,
					INDEX `UserId` (`UserId`) USING BTREE
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

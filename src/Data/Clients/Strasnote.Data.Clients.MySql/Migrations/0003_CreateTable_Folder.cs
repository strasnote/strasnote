// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(3, "Create table: main.folder")]
	public sealed class CreateTable_Folder_0003 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `main.folder` (
					`Id` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`FolderName` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
					`FolderParentId` BIGINT(20) UNSIGNED NULL DEFAULT NULL,
					`FolderCreated` DATETIME NOT NULL DEFAULT current_timestamp(),
					`FolderUpdated` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
					`UserId` BIGINT(20) UNSIGNED NOT NULL,
					PRIMARY KEY (`Id`) USING BTREE,
					INDEX `FolderParentId` (`FolderParentId`) USING BTREE,
					INDEX `UserId` (`UserId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE `main.folder`;");
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(4, "Create table: main.tag")]
	public sealed class CreateTable_Folder_0004 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `main.tag` (
					`Id` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`TagName` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
					`TagNameNormalised` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
					`TagCreated` DATETIME NOT NULL,
					`TagUpdated` DATETIME NOT NULL,
					`UserId` BIGINT(20) UNSIGNED NOT NULL DEFAULT '0',
					PRIMARY KEY (`Id`) USING BTREE,
					UNIQUE INDEX `TagNameNormalised_UserId` (`TagNameNormalised`, `UserId`) USING BTREE,
					INDEX `UserId` (`UserId`) USING BTREE,
					INDEX `TagNameNormalised` (`TagNameNormalised`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE IF EXISTS `main.tag`;");
		}
	}
}

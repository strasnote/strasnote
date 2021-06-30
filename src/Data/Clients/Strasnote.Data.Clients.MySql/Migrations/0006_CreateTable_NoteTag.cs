// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(6, "Create table: main.note_tag")]
	public sealed class CreateTable_NoteTag_0006 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `main.note_tag` (
					`Id` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`NoteId` BIGINT(20) UNSIGNED NOT NULL,
					`TagId` BIGINT(20) UNSIGNED NOT NULL,
					PRIMARY KEY (`Id`) USING BTREE,
					INDEX `NoteId` (`NoteId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");

			Execute(@"
				CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTagIdsForNote`(
					IN `NoteId` BIGINT,
					IN `UserId` BIGINT
				)
				LANGUAGE SQL
				NOT DETERMINISTIC
				CONTAINS SQL
				SQL SECURITY DEFINER
				COMMENT ''
				BEGIN

				SELECT `nt`.`TagId`
				FROM `main.note_tag` AS `nt`
				LEFT JOIN `main.note` AS `n` ON `nt`.`NoteId` = `n`.`Id`
				LEFT JOIN `main.tag` AS `t` ON `nt`.`TagId` = `t`.`Id`
				WHERE `nt`.`NoteId` = NoteId
				AND `n`.`UserId` = UserId
				AND `t`.`UserId` = UserId;

				END
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE `main.note_tag`;");
			Execute("DROP PROCEDURE `GetTagIdsForNote`;");
		}
	}
}

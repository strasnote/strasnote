// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(6, "Create main.note_tag table and associated procedures")]
	public sealed class Create_NoteTag_TableAndProcedures_0006 : Migration
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
				CREATE DEFINER=`root`@`localhost` PROCEDURE `AddTagToNote`(
					IN `TagId` BIGINT,
					IN `NoteId` BIGINT,
					IN `UserId` BIGINT
				)
				LANGUAGE SQL
				NOT DETERMINISTIC
				CONTAINS SQL
				SQL SECURITY DEFINER
				COMMENT ''
				BEGIN

				# Insert if:
				#	the note belongs to the user
				#	the tag belongs to the user
				#	the tag has not already been added to the note
				INSERT INTO `main.note_tag` (`NoteId`, `TagId`)
				SELECT NoteId, TagId
				WHERE 
					# The note belongs to the user
					1 = (
						SELECT COUNT(*) 
						FROM `main.note`
						WHERE `Id` = NoteId AND `UserId` = UserId
					)
					# The tag belongs to the user
					AND 1 = (
						SELECT COUNT(*) 
						FROM `main.tag`
						WHERE `Id` = TagId AND `UserId` = UserId
					) = 1
					# The tag has not already been added to the note
					AND 0 = (
						SELECT COUNT(*)
						FROM `main.note_tag` AS `nt`
						WHERE `NoteId` = NoteId
						AND `TagId` = TagId
					)
				;

				# Return whether or not the tag has been added to the note
				# (if the note already has the tag and nothing is inserted,
				# the procedure will still return 1)
				SELECT COUNT(*) FROM `main.note_tag` 
				WHERE `NoteId` = NoteId AND `TagId` = TagId;

				END
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
			Execute("DROP TABLE IF EXISTS `main.note_tag`;");
			Execute("DROP PROCEDURE IF EXISTS `AddTagToNote`;");
			Execute("DROP PROCEDURE IF EXISTS `GetTagIdsForNote`;");
		}
	}
}

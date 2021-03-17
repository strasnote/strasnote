// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(2, "Create stored procedures: User_RetrieveById and User_RetrieveByEmail")]
	public sealed class CreateProcedures_User_RetrieveByIdAndEmail_0002 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE DEFINER=`ben`@`%` PROCEDURE `User_RetrieveById`(
					IN `Id` BIGINT
				)
				LANGUAGE SQL
				NOT DETERMINISTIC
				READS SQL DATA
				SQL SECURITY DEFINER
				COMMENT 'Retrieve User by ID'
				BEGIN

				SELECT * FROM `auth.user` WHERE `auth.user`.`Id` = Id;

				END
			");

			Execute(@"
				CREATE PROCEDURE `User_RetrieveByEmail`(
					IN `Email` VARCHAR(255)
				)
				LANGUAGE SQL
				NOT DETERMINISTIC
				READS SQL DATA
				SQL SECURITY DEFINER
				COMMENT 'Retrieve User by Email Address'
				BEGIN

				SELECT * FROM `auth.user` WHERE `auth.user`.`Email` LIKE Email;

				END
			");
		}

		protected override void Down()
		{
			Execute("DROP PROCEDURE `User_RetrieveById`;");
			Execute("DROP PROCEDURE `User_RetrieveByEmail`;");
		}
	}
}

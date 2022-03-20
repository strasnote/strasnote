// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(8, "Now using MySqlConnector which maps bit(1) to ulong. We need to use tinyint unsigned instead.")]
	public sealed class Use_TinyInt_For_Bool : Migration
	{
		protected override void Down() => Execute(@"
			ALTER TABLE `auth.user`
			CHANGE COLUMN `LockoutEnabled` `LockoutEnabled` TINYINT NOT NULL DEFAULT '0' AFTER `LockoutEnd`;
		");
		protected override void Up() => Execute(@"
			ALTER TABLE `auth.user`
			CHANGE COLUMN `LockoutEnabled` `LockoutEnabled` BIT NOT NULL DEFAULT 0 AFTER `LockoutEnd`;
		");
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Migrate.Migrator_Tests
{
	public class ExecuteAsync_Tests
	{
		[Fact]
		public void If_Nuke_On_Startup_True_Runs_Client_Nuke_And_MigrateToLatest_And_Inserts_Test_Data()
		{
			// Arrange
			var (migrator, migrate, _, client, log, _) = Migrator_Setup.Get();
			migrate.NukeOnStartup = true;

			// Act
			migrator.ExecuteAsync();

			// Assert
			client.Received().Nuke();
			client.Received().MigrateToLatest();
			log.Received().Information("Inserting test data.");
		}

		[Fact]
		public void If_Migrate_To_Latest_True_Runs_Client_MigrateToLatest()
		{
			// Arrange
			var (migrator, migrate, _, client, _, _) = Migrator_Setup.Get();
			migrate.MigrateToLatestOnStartup = true;

			// Act
			migrator.ExecuteAsync();

			// Assert
			client.Received().MigrateToLatest();
		}
	}
}

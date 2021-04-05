// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Xunit;

namespace Strasnote.Data.Migrator_Tests
{
	public class ExecuteAsync_Tests
	{
		[Fact]
		public void If_Nuke_On_Startup_True_Runs_Client_Nuke_And_MigrateToLatest()
		{
			// Arrange
			var (migrator, migrate, _, client, _, _) = Migrator_Setup.Get();
			migrate.NukeOnStartup = true;

			// Act
			migrator.ExecuteAsync();

			// Assert
			client.Received().Nuke();
			client.Received().MigrateToLatest();
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

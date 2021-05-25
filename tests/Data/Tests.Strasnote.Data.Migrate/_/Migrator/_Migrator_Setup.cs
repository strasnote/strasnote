// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.Options;
using NSubstitute;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Migrate;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Data.Migrator_Tests
{
	public static class Migrator_Setup
	{
		public static (Migrator migrator, MigrateConfig migrate, UserConfig user, IDbClient client, ILog log, Repos repos) Get()
		{
			// Values
			var migrateConfig = new MigrateConfig();
			var migrate = Substitute.For<IOptions<MigrateConfig>>();
			migrate.Value.Returns(migrateConfig);

			var userConfig = new UserConfig();
			var user = Substitute.For<IOptions<UserConfig>>();
			user.Value.Returns(userConfig);

			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog<Migrator>>();
			var repos = new Repos(
				Substitute.For<IFolderRepository>(),
				Substitute.For<INoteRepository>(),
				Substitute.For<IUserRepository>()
			);

			// Create provider
			var provider = Substitute.For<IServiceProvider>();
			provider.GetService(typeof(IOptions<MigrateConfig>)).Returns(migrate);
			provider.GetService(typeof(IOptions<UserConfig>)).Returns(user);
			provider.GetService(typeof(IDbClient)).Returns(client);
			provider.GetService(typeof(ILog<Migrator>)).Returns(log);
			provider.GetService(typeof(IFolderRepository)).Returns(repos.Folder);
			provider.GetService(typeof(INoteRepository)).Returns(repos.Note);
			provider.GetService(typeof(IUserRepository)).Returns(repos.User);

			// Return
			return (new Migrator(provider), migrateConfig, userConfig, client, log, repos);
		}

		public sealed record Repos(
			IFolderRepository Folder,
			INoteRepository Note,
			IUserRepository User
		);
	}
}

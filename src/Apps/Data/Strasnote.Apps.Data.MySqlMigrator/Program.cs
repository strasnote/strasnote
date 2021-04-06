// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.Apps.Data.MySqlMigrator;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Migrate;

await Strasnote.AppBase.Program.MainAsync<HostBuilder>(args, async (_, services) =>
{
	Console.WriteLine("= Strasnote: MySQL Migrator =");

	bool loop = true;
	while (loop)
	{
		// Get the action
		Console.WriteLine("Please enter the action you would like to take:");
		Console.WriteLine("[migrate] [nuke] [exit]");
		switch (Console.ReadLine())
		{
			// Migrate requires a version number
			case "migrate":
				var client = services.GetRequiredService<ISqlClient>();
				Migrate.Execute(client);
				break;

			case "nuke":
				var migrator = new Migrator(services);
				await Nuke.ExecuteAsync(migrator).ConfigureAwait(false);
				break;

			// Setting loop to false will stop the next iteration
			case "exit":
				loop = false;
				break;
		}
	}

	Console.WriteLine("Goodbye!");
}).ConfigureAwait(false);

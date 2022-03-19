// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.AppBase;
using Strasnote.Apps.Data.MySqlMigrator;
using Strasnote.Auth.Data.Extensions;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Clients.MySql;
using Strasnote.Data.Migrate;
using Strasnote.Notes.Data;

// =========================================================================
// BUILD
// =========================================================================

var builder = Host.CreateDefaultBuilder();
builder.ConfigureStrasnote(args, (_, services) =>
{
	services.AddAuthData<MySqlClient>();
	services.AddNotesData<MySqlClient>();
});
var app = builder.Build();

// =========================================================================
// RUN
// =========================================================================

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
			var client = app.Services.GetRequiredService<ISqlClient>();
			Migrate.Execute(client);
			break;

		case "nuke":
			var migrator = new Migrator(app.Services);
			await Nuke.ExecuteAsync(migrator).ConfigureAwait(false);
			break;

		// Setting loop to false will stop the next iteration
		case "exit":
			loop = false;
			break;
	}
}

Console.WriteLine("Goodbye!");

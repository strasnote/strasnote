// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Strasnote.Data.Migrate;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public static class Nuke
	{
		/// <summary>
		/// Nuke the database, migrate to latest, and insert test data
		/// </summary>
		/// <param name="migrator">Migrator</param>
		public static async Task ExecuteAsync(Migrator migrator)
		{
			// Make sure the user really wants to do this
			Console.WriteLine("== Nuking database == ");
			Console.WriteLine("Are you SURE you want to do this?");
			Console.WriteLine("** Everything ** will be deleted and test data recreated.");
			Console.WriteLine("Type 'yes' to proceed, anything else to quit.");
			if (Console.ReadLine()?.Trim().ToLower() != "yes")
			{
				return;
			}

			try
			{
				await migrator.Nuke().ConfigureAwait(false);
				Console.WriteLine("Nuke complete.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error nuking database: {0}", ex);
			}
		}
	}
}

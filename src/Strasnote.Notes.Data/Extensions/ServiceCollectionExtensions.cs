// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.DependencyInjection;
using Strasnote.Data.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Data
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register the Notes data services
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		public static IServiceCollection AddNotesData<TClient>(this IServiceCollection services)
			where TClient : class, ISqlClient
		{
			// Add database client
			services.AddTransient<ISqlClient, TClient>();
			services.AddTransient<IDbClient>(s => s.GetRequiredService<ISqlClient>());

			// Add repositories
			services.AddTransient<IFolderRepository, FolderSqlRepository>();
			services.AddTransient<INoteRepository, NoteSqlRepository>();
			services.AddTransient<INoteTagRepository, NoteTagSqlRepository>();
			services.AddTransient<ITagRepository, TagSqlRepository>();

			return services;
		}
	}
}

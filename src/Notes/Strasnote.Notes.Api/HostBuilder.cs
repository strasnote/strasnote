// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.IO;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.AppBase;
using Strasnote.AppBase.ModelBinding;
using Strasnote.Auth.Data.Extensions;
using Strasnote.Auth.Extensions;
using Strasnote.Data.Clients.MySql;
using Strasnote.Data.TypeHandlers;
using Strasnote.Notes.Data;

namespace Strasnote.Notes.Api
{
	/// <summary>
	/// Notes API Host Builder
	/// </summary>
	public sealed class HostBuilder : WebAppHostBuilder
	{
		/// <inheritdoc/>
		protected override void ConfigureServices(IWebHostEnvironment host, IServiceCollection services, IConfiguration config)
		{
			base.ConfigureServices(host, services, config);

			// MVC
			services.AddControllers(opt => opt.ModelBinderProviders.Insert(0, new RouteIdModelBinderProvider()));

			// Auth
			services.AddAuth(config);
			services.AddAuthData<MySqlClient>();
			services.AddApiVersioning();

			// Notes
			services.AddNotesData<MySqlClient>();

			// Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "Strasnote Notes API", Version = "v1" });

				c.SchemaGeneratorOptions = new() { SchemaIdSelector = type => type.FullName };

				var filePath = Path.Combine(System.AppContext.BaseDirectory, "Strasnote.Notes.Api.xml");
				c.IncludeXmlComments(filePath);
			});

			// Add DateTime handler
			SqlMapper.AddTypeHandler(new DateTimeTypeHandler());
		}

		/// <inheritdoc/>
		protected override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("v1/swagger.json", "Strasnote Notes API v1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.AppBase;
using Strasnote.AppBase.ModelBinding;
using Strasnote.Auth.Data.Extensions;
using Strasnote.Auth.Extensions;
using Strasnote.Data.Clients.MySql;
using Strasnote.Notes.Data;

// =========================================================================
// BUILD
// =========================================================================

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureStrasnote((ctx, services) =>
{
	// MVC
	services.AddControllers(opt => opt.ModelBinderProviders.Insert(0, new RouteIdModelBinderProvider()));
	services.AddApiVersioning();

	// Auth
	services.AddAuth(ctx.Configuration);
	services.AddAuthData<MySqlClient>();

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
});
var app = builder.Build();

// =========================================================================
// CONFIGURE
// =========================================================================

app.UseAppDefaults();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Strasnote Notes API v1"));
app.UseEndpoints(endpoints => endpoints.MapControllers());

// =========================================================================
// RUN
// =========================================================================

await app.RunAppAsync();

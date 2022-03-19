// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Strasnote.AppBase;
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
	services.AddControllers();
	services.AddApiVersioning();

	// Auth
	services.AddAuth(ctx.Configuration);
	services.AddAuthData<MySqlClient>();

	// Notes (required for migrator)
	services.AddNotesData<MySqlClient>();
});
var app = builder.Build();

// =========================================================================
// CONFIGURE
// =========================================================================

app.UseStrasnoteDefaults();
app.UseEndpoints(endpoints => endpoints.MapControllers());

// =========================================================================
// RUN
// =========================================================================

await app.RunStrasnoteAsync();

// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.AppBase;

// =========================================================================
// BUILD
// =========================================================================

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureStrasnote((ctx, services) =>
{
	services.AddRazorPages();
});

var app = builder.Build();

// =========================================================================
// CONFIGURE
// =========================================================================

app.UseAppDefaults();
app.MapRazorPages();

// =========================================================================
// RUN
// =========================================================================

await app.RunAppAsync();

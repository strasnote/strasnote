// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.AppBase;
using Strasnote.Auth.Extensions;
using Strasnote.Data.Clients.MySql;

namespace Strasnote.Auth.Api
{
	public sealed class HostBuilder : WebAppHostBuilder
	{
		/// <inheritdoc/>
		protected override void ConfigureServices(IWebHostEnvironment host, IServiceCollection services, IConfiguration config)
		{
			base.ConfigureServices(host, services, config);

			// MVC
			services.AddControllers();

			// Auth
			services.AddAuth(config);
			services.AddAuthData<MySqlClient>();
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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

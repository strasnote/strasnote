// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.AppBase;
using Strasnote.Auth.Extensions;
using Strasnote.Data.Clients.MySql;

namespace Strasnote.Apps.Data.MySqlMigrator
{
	public sealed class HostBuilder : GenericAppHostBuilder
	{
		protected override void ConfigureServices(IHostEnvironment host, IServiceCollection services, IConfiguration config)
		{
			base.ConfigureServices(host, services, config);

			services.AddAuthData<MySqlClient>();
		}
	}
}

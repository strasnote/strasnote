// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.Auth.Config;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Auth.Extensions;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Clients.MySql;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration) =>
			Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(Configuration);

			// ToDo: add some IServiceCollection extensions
			services
				.AddIdentityCore<UserEntity>(options =>
				{
					options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
				})
				.AddDefaultTokenProviders();

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(config =>
				{
					config.RequireHttpsMetadata = true;
					config.SaveToken = true;
					config.TokenValidationParameters = new AuthConfigTokenValidationParameters(Configuration);
				});

			services.AddControllers();

			services.AddTransient<ISqlClient, MySqlClient>();
			services.AddTransient<IDbClient>(s => s.GetRequiredService<ISqlClient>());

			services.AddTransient<IUserRepository, UserSqlRepository>();
			services.AddTransient<IUserStore<UserEntity>, UserStore>();

			services.AddTransient<IRefreshTokenRepository, RefreshTokenSqlRepository>();

			//services.AddAuthDataFakeServices();
			services.AddAuthServices(Configuration);

			services.Configure<AuthConfig>(Configuration.GetSection(AuthConfig.AppSettingsSectionName));
			services.Configure<DbConfig>(Configuration.GetSection(DbConfig.AppSettingsSectionName));
			services.Configure<MigrateConfig>(Configuration.GetSection(MigrateConfig.AppSettingsSectionName));
			services.Configure<UserConfig>(Configuration.GetSection(UserConfig.AppSettingsSectionName));

			services.AddTransient<JwtSecurityTokenHandler>();

			services.AddHttpContextAccessor();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

			app.ApplicationServices.GetRequiredService<ILog<Startup>>().Information("Application Configured.");
		}
	}
}
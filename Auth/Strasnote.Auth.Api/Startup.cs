using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strasnote.Auth.Data.Abstracts;
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

			//services
			//	.AddIdentity<UserEntity, RoleEntity>()
			//	.AddDefaultTokenProviders();

			services
				.AddAuthentication()
				.AddJwtBearer();

			services.AddControllers();

			services.AddScoped<IUserContext, Data.Fake.UserContext>();
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.ApplicationServices.GetService<ILog<Startup>>()?.Information("Application Configured.");
		}
	}
}

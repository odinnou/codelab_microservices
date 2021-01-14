using Catalogue.API.Configuration;
using Catalogue.API.Grpc;
using Catalogue.API.Infrastructure.Migrations;
using Common.API.Documentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Catalogue.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            AppSettings appSettings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

            services.AddGrpc();

            services.AddControllers();

            services.AddSwagger(appSettings.DeployedVersion);
            services.AddDependencies(appSettings);
            services.AddHostedService<MigratorHostedService>();
        }

        public void Configure(IApplicationBuilder app, IOptions<AppSettings> appSettings)
        {
            if (appSettings.Value.EnableSwagger)
            {
                app.UseSwaggerConfig(appSettings.Value.DeployedVersion, appSettings.Value.RoutePrefix, appSettings.Value.HttpRequestScheme);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<ProduitService>();
            });
        }
    }
}

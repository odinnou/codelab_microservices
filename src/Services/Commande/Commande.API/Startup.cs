using Common.API.Documentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Commande.API.Configuration;
using System.Text.Json.Serialization;

namespace Commande.API
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

            services.AddAuthentications(appSettings);
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwagger(appSettings.DeployedVersion);

            services.AddDependencies(appSettings);
            services.AddThirdParties(appSettings);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IOptions<AppSettings> appSettings)
        {
            if (appSettings.Value.EnableSwagger)
            {
                app.UseSwaggerConfig(appSettings.Value.DeployedVersion, appSettings.Value.RoutePrefix, appSettings.Value.HttpRequestScheme);
            }

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

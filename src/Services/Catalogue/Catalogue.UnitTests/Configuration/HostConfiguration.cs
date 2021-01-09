using Catalogue.API;
using Catalogue.API.Configuration;
using Catalogue.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogue.UnitTests.Configuration
{
    public static class HostConfiguration
    {
        public static IWebHostBuilder ValidHostBuilder()
        {
            return new WebHostBuilder()
                .UseContentRoot(".")
                .UseEnvironment(AppSettings.TEST_ENVIRONMENT)
        .ConfigureAppConfiguration((builderContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        })
        .ConfigureServices(services =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<CatalogueContext>(options => options.UseLazyLoadingProxies().UseSqlite(connection).UseSnakeCaseNamingConvention());
        })
                .UseStartup<Startup>();
        }
    }
}

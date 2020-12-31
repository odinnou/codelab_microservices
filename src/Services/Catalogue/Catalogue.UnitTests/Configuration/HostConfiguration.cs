using Catalogue.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Catalogue.UnitTests.Configuration
{
    public static class HostConfiguration
    {
        public static IWebHostBuilder ValidHostBuilder()
        {
            return new WebHostBuilder()
                .UseContentRoot(".")
                .UseEnvironment("test")
        .ConfigureAppConfiguration((builderContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        })
        .ConfigureServices(services =>
        {
            // AVANT le ConfigureServices du Startup
        }).UseStartup<Startup>();
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panier.API;

namespace Panier.UnitTests.Configuration
{
    public static class HostConfiguration
    {
        public static IWebHostBuilder AnonymousHostBuilder()
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

        public static IWebHostBuilder AuthenticatedHostBuilder(bool emailVerified = true, string uid = "M3Gid5WY9sU5TDFzUdWDBPfX2o02", string type = "client")
        {
            return new WebHostBuilder().UseContentRoot(".")
                                       .UseEnvironment("test")
                                       .ConfigureAppConfiguration((builderContext, config) =>
                                       {
                                           config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                                       })
                                       .ConfigureServices(services =>
                                       {
                                           services.AddAuthentication(options =>
                                           {
                                               options.DefaultAuthenticateScheme = "Test Scheme"; // has to match scheme in TestAuthenticationExtensions
                                               options.DefaultChallengeScheme = "Test Scheme";
                                           }).AddTestAuth(claims => { claims.Uid = uid; claims.EmailVerified = emailVerified; claims.UserType = type; });
                                       })
                                       .ConfigureTestServices(services =>
                                       {
                                           // APRES le ConfigureServices du Startup
                                       })
                                       .UseStartup<Startup>();
        }
    }
}

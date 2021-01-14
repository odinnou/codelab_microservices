using Catalogue.API.Infrastructure;
using Catalogue.API.Repositories;
using Catalogue.API.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogue.API.Configuration
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, AppSettings appSettings)
        {
            #region Database

            services.AddDbContext<CatalogueContext>(options => options.UseLazyLoadingProxies().UseNpgsql(appSettings.DbConnection).UseSnakeCaseNamingConvention());

            #endregion

            #region Use Cases

            services.AddTransient<IProduitFetcher, ProduitFetcher>();

            #endregion

            #region Repositories

            services.AddTransient<IFamilleRepository, FamilleRepository>();
            services.AddTransient<IProduitRepository, ProduitRepository>();

            #endregion

            return services;
        }
    }
}

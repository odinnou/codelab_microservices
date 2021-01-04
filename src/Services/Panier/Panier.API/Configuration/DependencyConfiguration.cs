using Microsoft.Extensions.DependencyInjection;
using Panier.API.Models;
using Panier.API.UseCases;
using Panier.API.UseCases.PanierManager;

namespace Panier.API.Configuration
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, AppSettings appSettings)
        {
            #region Cache

            services.AddStackExchangeRedisCache(action =>
            {
                action.Configuration = appSettings.CacheConfiguration.ConnectionString;
                action.InstanceName = appSettings.CacheConfiguration.InstanceName;
            });

            services.AddSingleton<PanierCache>();

            #endregion

            #region Use Cases

            services.AddTransient<IPanierManager, StatelessPanierManager>();
            services.AddTransient<IPanierManager, StatefulPanierManager>();
            services.AddTransient<IClaimAccessor, ClaimAccessor>();

            #endregion

            return services;
        }
    }
}

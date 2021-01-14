using Microsoft.Extensions.DependencyInjection;
using System;
using static Catalogue.API.ProduitProducer;

namespace Panier.API.Configuration
{
    public static class ThirdPartyConfiguration
    {
        public static IServiceCollection AddThirdParties(this IServiceCollection services, AppSettings appSettings)
        {
            // Autorise l'accès à des ressources gRPC en HTTP.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            #region gRPC

            services.AddGrpcClient<ProduitProducerClient>(client => { client.Address = new Uri(appSettings.CatalogueServiceConfiguration.Url); });

            #endregion

            return services;
        }
    }
}

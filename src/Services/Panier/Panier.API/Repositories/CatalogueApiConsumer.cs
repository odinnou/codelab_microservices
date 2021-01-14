using System.Threading.Tasks;
using static Catalogue.API.ProduitProducer;

namespace Panier.API.Repositories
{
    public class CatalogueApiConsumer : ICatalogueApiConsumer
    {
        private readonly ProduitProducerClient produitProducerClient;

        public CatalogueApiConsumer(ProduitProducerClient produitProducerClient)
        {
            this.produitProducerClient = produitProducerClient;
        }

        public async Task<bool> CheckDisponibility(string reference)
        {
            try
            {
                Catalogue.API.CheckDisponibilityRequest request = new Catalogue.API.CheckDisponibilityRequest { Reference = reference };
                Catalogue.API.CheckDisponibilityResponse response = await produitProducerClient.CheckDisponibilityByReferenceAsync(request);

                return response.IsAvailable;
            }
            catch
            {
                return false;
            }
        }
    }
}

using Panier.API.Infrastructure.Exceptions;
using Panier.API.Repositories;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public abstract class APanierManager
    {
        protected readonly IClaimAccessor iClaimAccessor;
        private readonly ICatalogueApiConsumer iCatalogueApiConsumer;

        protected APanierManager(IClaimAccessor iClaimAccessor, ICatalogueApiConsumer iCatalogueApiConsumer)
        {
            this.iClaimAccessor = iClaimAccessor;
            this.iCatalogueApiConsumer = iCatalogueApiConsumer;
        }

        protected async Task CheckProduitDisponibility(string reference)
        {
            bool isAvailable = await iCatalogueApiConsumer.CheckDisponibility(reference);

            if (!isAvailable)
            {
                throw new ProduitNotAvailableException(reference);
            }
        }
    }
}

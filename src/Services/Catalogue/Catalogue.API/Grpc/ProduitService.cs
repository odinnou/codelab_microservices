using Catalogue.API.Models;
using Catalogue.API.UseCases;
using Grpc.Core;
using System.Threading.Tasks;
using static Catalogue.API.ProduitProducer;

namespace Catalogue.API.Grpc
{
    public class ProduitService : ProduitProducerBase
    {
        private readonly IProduitFetcher iProduitFetcher;

        public ProduitService(IProduitFetcher iProduitFetcher)
        {
            this.iProduitFetcher = iProduitFetcher;
        }

        public override async Task<CheckDisponibilityResponse> CheckDisponibilityByReference(CheckDisponibilityRequest request, ServerCallContext context)
        {
            Produit produit = await iProduitFetcher.GetByReference(request.Reference);

            return new CheckDisponibilityResponse { IsAvailable = produit.IsAvailable };
        }
    }
}

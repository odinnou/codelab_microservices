using Catalogue.API.Models;
using Catalogue.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.API.UseCases
{
    public class ProduitFetcher : IProduitFetcher
    {
        private readonly IProduitRepository iProduitRepository;

        public ProduitFetcher(IProduitRepository iProduitRepository)
        {
            this.iProduitRepository = iProduitRepository;
        }

        public async Task<IDictionary<Famille, IEnumerable<Produit>>> GroupByFamilles()
        {
            IEnumerable<Produit> produits = await iProduitRepository.GetAll();

            return produits.GroupBy(produit => produit.Famille)
                           .ToDictionary(group => group.Key, group => group.AsEnumerable());
        }
    }
}

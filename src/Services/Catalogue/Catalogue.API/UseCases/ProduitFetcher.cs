using Catalogue.API.Models;
using Catalogue.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.API.UseCases
{
    public class ProduitFetcher : IProduitFetcher
    {
        private readonly IFamilleRepository iProduitRepository;

        public ProduitFetcher(IFamilleRepository iProduitRepository)
        {
            this.iProduitRepository = iProduitRepository;
        }

        public async Task<Dictionary<string, ICollection<Produit>>> GroupByFamilles()
        {
            IEnumerable<Famille> familles = await iProduitRepository.GetAll();

            return familles.ToDictionary(group => group.Nom, group => group.Produits);
        }
    }
}

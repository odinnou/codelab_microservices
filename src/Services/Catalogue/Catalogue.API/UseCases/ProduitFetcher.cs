using Catalogue.API.Infrastructure.Exceptions;
using Catalogue.API.Models;
using Catalogue.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.API.UseCases
{
    public class ProduitFetcher : IProduitFetcher
    {
        private readonly IFamilleRepository iFamilleRepository;
        private readonly IProduitRepository iProduitRepository;

        public ProduitFetcher(IFamilleRepository iFamilleRepository, IProduitRepository iProduitRepository)
        {
            this.iFamilleRepository = iFamilleRepository;
            this.iProduitRepository = iProduitRepository;
        }

        public async Task<Produit> GetByReference(string reference)
        {
            Produit? produit = await iProduitRepository.GetByReference(reference);

            if(produit == null)
            {
                throw new ProduitNotFoundException(reference);
            }

            return produit;
        }

        public async Task<Dictionary<string, ICollection<Produit>>> GroupByFamilles()
        {
            IEnumerable<Famille> familles = await iFamilleRepository.GetAll();

            return familles.ToDictionary(group => group.Nom, group => group.Produits);
        }
    }
}

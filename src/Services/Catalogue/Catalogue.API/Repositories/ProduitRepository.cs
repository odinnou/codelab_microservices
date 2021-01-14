using Catalogue.API.Infrastructure;
using Catalogue.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public class ProduitRepository : BaseRepository, IProduitRepository
    {
        public ProduitRepository(CatalogueContext catalogueContext) : base(catalogueContext)
        {
        }

        public async Task<Produit?> GetByReference(string reference)
        {
            return await catalogueContext.Produits.SingleOrDefaultAsync(produit => produit.Reference == reference);
        }
    }
}

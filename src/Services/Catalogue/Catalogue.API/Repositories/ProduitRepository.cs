using Catalogue.API.Infrastructure;
using Catalogue.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public class ProduitRepository : BaseRepository, IProduitRepository
    {
        public ProduitRepository(CatalogueContext catalogueContext) : base(catalogueContext)
        {
        }

        public async Task<IEnumerable<Produit>> GetAll()
        {
            return await catalogueContext.Produits.ToListAsync();
        }
    }
}

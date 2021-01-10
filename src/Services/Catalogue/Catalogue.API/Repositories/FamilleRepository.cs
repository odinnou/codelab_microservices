using Catalogue.API.Infrastructure;
using Catalogue.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public class FamilleRepository : BaseRepository, IFamilleRepository
    {
        public FamilleRepository(CatalogueContext catalogueContext) : base(catalogueContext)
        {
        }

        public async Task<IEnumerable<Famille>> GetAll()
        {
            return await catalogueContext.Familles
                                         .Include(nameof(Famille.Produits))
                                         .ToListAsync();
        }
    }
}

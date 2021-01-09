using Catalogue.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.UseCases
{
    public interface IProduitFetcher
    {
        Task<IDictionary<Famille, IEnumerable<Produit>>> GroupByFamilles();
    }
}

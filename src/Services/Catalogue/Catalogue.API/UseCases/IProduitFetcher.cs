using Catalogue.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.UseCases
{
    public interface IProduitFetcher
    {
        Task<Dictionary<string, ICollection<Produit>>> GroupByFamilles();
        Task<Produit> GetByReference(string reference);
    }
}

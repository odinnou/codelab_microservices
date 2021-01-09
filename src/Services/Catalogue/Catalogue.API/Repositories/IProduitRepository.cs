using Catalogue.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public interface IProduitRepository
    {
        Task<IEnumerable<Produit>> GetAll();
    }
}

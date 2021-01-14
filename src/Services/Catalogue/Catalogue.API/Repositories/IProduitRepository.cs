using Catalogue.API.Models;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public interface IProduitRepository
    {
        Task<Produit?> GetByReference(string reference);
    }
}

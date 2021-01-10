using Catalogue.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public interface IFamilleRepository
    {
        Task<IEnumerable<Famille>> GetAll();
    }
}

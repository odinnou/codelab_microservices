using System.Threading.Tasks;

namespace Panier.API.Repositories
{
    public interface ICatalogueApiConsumer
    {
        Task<bool> CheckDisponibility(string reference);
    }
}

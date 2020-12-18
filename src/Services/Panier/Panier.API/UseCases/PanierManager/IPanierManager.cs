using Panier.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public interface IPanierManager
    {
        CacheMode CacheMode { get; }
        Task<List<PanierItem>> Fetch(string userId);
        Task<List<PanierItem>> Append(PanierItem panierItem);
    }
}

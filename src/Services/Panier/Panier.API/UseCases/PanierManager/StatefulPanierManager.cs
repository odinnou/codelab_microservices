using Panier.API.Models;
using Panier.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public class StatefulPanierManager : APanierManager, IPanierManager
    {
        private readonly PanierCache panierCache;

        public StatefulPanierManager(PanierCache panierCache, IClaimAccessor iClaimAccessor, ICatalogueApiConsumer iCatalogueApiConsumer) : base(iClaimAccessor, iCatalogueApiConsumer)
        {
            this.panierCache = panierCache;
        }

        public CacheMode CacheMode => CacheMode.Stateful;

        public async Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            await CheckProduitDisponibility(panierItem.Product);

            string userId = iClaimAccessor.GetUidFromClaims();

            if (!panierCache.Content.ContainsKey(userId))
            {
                panierCache.Content.Add(userId, new List<PanierItem>());
            }

            panierCache.Content[userId].Add(panierItem);

            return await Fetch();
        }

        public Task<List<PanierItem>> Fetch()
        {
            string userId = iClaimAccessor.GetUidFromClaims();

            return Task.FromResult(panierCache.Content.ContainsKey(userId) ? panierCache.Content[userId] : new List<PanierItem>());
        }
    }
}

using Panier.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public class StatefulPanierManager : IPanierManager
    {
        private readonly PanierCache panierCache;
        private readonly IClaimAccessor iClaimAccessor;

        public StatefulPanierManager(PanierCache panierCache, IClaimAccessor iClaimAccessor)
        {
            this.panierCache = panierCache;
            this.iClaimAccessor = iClaimAccessor;
        }

        public CacheMode CacheMode => CacheMode.Stateful;

        public Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            string userId = iClaimAccessor.GetUidFromClaims();

            if (!panierCache.Content.ContainsKey(userId))
            {
                panierCache.Content.Add(userId, new List<PanierItem>());
            }

            panierCache.Content[userId].Add(panierItem);

            return Fetch();
        }

        public Task<List<PanierItem>> Fetch()
        {
            string userId = iClaimAccessor.GetUidFromClaims();

            return Task.FromResult(panierCache.Content.ContainsKey(userId) ? panierCache.Content[userId] : new List<PanierItem>());
        }
    }
}

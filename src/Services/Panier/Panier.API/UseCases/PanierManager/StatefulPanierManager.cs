using Panier.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public class StatefulPanierManager : IPanierManager
    {
        private readonly PanierCache panierCache;

        public StatefulPanierManager(PanierCache panierCache)
        {
            this.panierCache = panierCache;
        }

        public CacheMode CacheMode => CacheMode.Stateful;

        public Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            if (!panierCache.Content.ContainsKey(panierItem.UserId))
            {
                panierCache.Content.Add(panierItem.UserId, new List<PanierItem>());
            }

            panierCache.Content[panierItem.UserId].Add(panierItem);

            return Fetch(panierItem.UserId);
        }

        public Task<List<PanierItem>> Fetch(string userId)
        {
            return Task.FromResult(panierCache.Content.ContainsKey(userId) ? panierCache.Content[userId] : new List<PanierItem>());
        }
    }
}

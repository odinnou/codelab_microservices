using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panier.API.Configuration;
using Panier.API.Models;
using Panier.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public class StatelessPanierManager : APanierManager, IPanierManager
    {
        private readonly IDistributedCache iDistributedCache;
        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;

        public StatelessPanierManager(IDistributedCache iDistributedCache, IOptions<AppSettings> appSettings, IClaimAccessor iClaimAccessor, ICatalogueApiConsumer iCatalogueApiConsumer) : base(iClaimAccessor, iCatalogueApiConsumer)
        {
            this.iDistributedCache = iDistributedCache;
            distributedCacheEntryOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appSettings.Value.CacheConfiguration.TimeToLive) };
        }

        public CacheMode CacheMode => CacheMode.Stateless;

        public async Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            await CheckProduitDisponibility(panierItem.Product);

            List<PanierItem> items = await Fetch();

            items.Add(panierItem);

            await StoreItemsInDistributedCache(items);

            return items;
        }

        public async Task<List<PanierItem>> Fetch()
        {
            string serialized = await GetItemsFromDistributedCache();

            if (string.IsNullOrWhiteSpace(serialized))
            {
                return new List<PanierItem>();
            }

            return JsonConvert.DeserializeObject<List<PanierItem>>(serialized);
        }

        private async Task StoreItemsInDistributedCache(List<PanierItem> items)
        {
            string userId = iClaimAccessor.GetUidFromClaims();

            await iDistributedCache.SetStringAsync(userId, JsonConvert.SerializeObject(items), distributedCacheEntryOptions);
        }

        private async Task<string> GetItemsFromDistributedCache()
        {
            string userId = iClaimAccessor.GetUidFromClaims();
            return await iDistributedCache.GetStringAsync(userId);
        }
    }
}

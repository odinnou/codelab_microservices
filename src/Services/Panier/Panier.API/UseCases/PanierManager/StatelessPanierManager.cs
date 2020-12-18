using Catalogue.API.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panier.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panier.API.UseCases.PanierManager
{
    public class StatelessPanierManager : IPanierManager
    {
        private readonly IDistributedCache iDistributedCache;
        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;

        public StatelessPanierManager(IDistributedCache iDistributedCache, IOptions<AppSettings> appSettings)
        {
            this.iDistributedCache = iDistributedCache;
            distributedCacheEntryOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appSettings.Value.CacheConfiguration.TimeToLive) };
        }

        public CacheMode CacheMode => CacheMode.Stateless;

        public async Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            List<PanierItem> items = await Fetch(panierItem.UserId);

            items.Add(panierItem);

            await iDistributedCache.SetStringAsync(panierItem.UserId, JsonConvert.SerializeObject(items), distributedCacheEntryOptions);

            return items;
        }

        public async Task<List<PanierItem>> Fetch(string userId)
        {
            string serialized = await iDistributedCache.GetStringAsync(userId);

            if (string.IsNullOrWhiteSpace(serialized))
            {
                return new List<PanierItem>();
            }

            return JsonConvert.DeserializeObject<List<PanierItem>>(serialized);
        }
    }
}

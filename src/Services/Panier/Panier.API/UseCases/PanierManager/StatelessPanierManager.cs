using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panier.API.Configuration;
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
        private readonly IClaimAccessor iClaimAccessor;

        public StatelessPanierManager(IDistributedCache iDistributedCache, IOptions<AppSettings> appSettings, IClaimAccessor iClaimAccessor)
        {
            this.iDistributedCache = iDistributedCache;
            this.iClaimAccessor = iClaimAccessor;
            distributedCacheEntryOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(appSettings.Value.CacheConfiguration.TimeToLive) };
        }

        public CacheMode CacheMode => CacheMode.Stateless;

        public async Task<List<PanierItem>> Append(PanierItem panierItem)
        {
            List<PanierItem> items = await Fetch();

            items.Add(panierItem);

            string userId = iClaimAccessor.GetUidFromClaims();
            await iDistributedCache.SetStringAsync(userId, JsonConvert.SerializeObject(items), distributedCacheEntryOptions);

            return items;
        }

        public async Task<List<PanierItem>> Fetch()
        {
            string userId = iClaimAccessor.GetUidFromClaims();
            string serialized = await iDistributedCache.GetStringAsync(userId);

            if (string.IsNullOrWhiteSpace(serialized))
            {
                return new List<PanierItem>();
            }

            return JsonConvert.DeserializeObject<List<PanierItem>>(serialized);
        }
    }
}

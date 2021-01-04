using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panier.API.Infrastructure;
using Panier.API.Models;
using Panier.API.UseCases.PanierManager;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panier.API.Controllers
{
    [Authorize(ONLY_CLIENT_POLICY)]
    public class PanierController : CommonController
    {
        private readonly Dictionary<CacheMode, IPanierManager> iPanierManagers;

        public PanierController(IEnumerable<IPanierManager> iPanierManagers)
        {
            this.iPanierManagers = iPanierManagers.ToDictionary(_ => _.CacheMode);
        }

        /*
         * curl -X GET 'http://localhost:{port}/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
         */
        [HttpGet("{cacheMode:required}")]
        public async Task<IEnumerable<PanierItem>> FetchPanierItems(CacheMode cacheMode)
        {
            return await iPanierManagers[cacheMode].Fetch();
        }

        /*
         * curl -X POST 'http://localhost:{port}/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........' -H 'Content-Type: application/json' --data-raw '{"product":"PS5"}'
         */
        [HttpPost("{cacheMode:required}")]
        public async Task<IEnumerable<PanierItem>> AppendPanierItem(CacheMode cacheMode, PanierItem panierItem)
        {
            return await iPanierManagers[cacheMode].Append(panierItem);
        }
    }
}

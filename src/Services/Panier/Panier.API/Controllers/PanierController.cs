using Microsoft.AspNetCore.Mvc;
using Panier.API.Models;
using Panier.API.UseCases.PanierManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Panier.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PanierController : ControllerBase
    {
        private readonly Dictionary<CacheMode, IPanierManager> iPanierManagers;

        public PanierController(IEnumerable<IPanierManager> iPanierManagers)
        {
            this.iPanierManagers = iPanierManagers.ToDictionary(_ => _.CacheMode);
        }

        /*
         * curl -X GET 'http://localhost:{port}/panier/{stateless}/fanboy'
         */
        [HttpGet("{cacheMode:required}/{userId:required}")]
        public async Task<IEnumerable<PanierItem>> FetchPanierItems(CacheMode cacheMode, string userId)
        {
            return await iPanierManagers[cacheMode].Fetch(userId);
        }

        /*
         * curl -X POST 'http://localhost:{port}/panier/stateless' -H 'Content-Type: application/json' --data-raw '{"userId":"fanboy","product":"PS5"}'
         */
        [HttpPost("{cacheMode:required}")]
        public async Task<IEnumerable<PanierItem>> AppendPanierItem(CacheMode cacheMode, PanierItem panierItem)
        {
            return await iPanierManagers[cacheMode].Append(panierItem);
        }
    }
}

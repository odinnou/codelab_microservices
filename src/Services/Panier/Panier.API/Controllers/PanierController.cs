using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panier.API.Infrastructure;
using Panier.API.Models;
using Panier.API.UseCases.PanierManager;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Panier.API.Controllers
{
    [Authorize(ONLY_CLIENT_POLICY)] // oblige le user_type à client et conserve la nécessité d'avoir validé son email
    public class PanierController : CommonController
    {
        private readonly Dictionary<CacheMode, IPanierManager> iPanierManagers;

        public PanierController(IEnumerable<IPanierManager> iPanierManagers)
        {
            this.iPanierManagers = iPanierManagers.ToDictionary(_ => _.CacheMode);
        }

        /// <summary>
        /// Retourne le contenu du panier stateless ou stateful (client only)
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:{port}/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
        /// </remarks>
        /// <param name="cacheMode">Type de cache : Stateless ou Stateful</param>
        /// <response code="200">OK, la liste produits dans le panier</response>
        /// <response code="400">Requête invalide</response>
        /// <response code="401">Client non connecté</response>
        /// <response code="500">Exception non gérée</response>
        [HttpGet("{cacheMode:required}")]
        [ProducesResponseType(typeof(IEnumerable<PanierItem>), Status200OK)]
        [ProducesResponseType(typeof(void), Status400BadRequest)]
        [ProducesResponseType(typeof(void), Status401Unauthorized)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public async Task<IEnumerable<PanierItem>> FetchPanierItems(CacheMode cacheMode)
        {
            return await iPanierManagers[cacheMode].Fetch();
        }

        /// <summary>
        /// Ajoute un produit au panier stateless ou stateful et retourne son contenu mis à jour (client only)
        /// </summary>
        /// <remarks>
        /// curl -X POST 'http://localhost:{port}/panier/stateless' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........' -H 'Content-Type: application/json' --data-raw '{"product":"PS5"}'
        /// </remarks>
        /// <param name="cacheMode">Type de cache : Stateless ou Stateful</param>
        /// <param name="panierItem">Produit à ajouter au panier</param>
        /// <response code="200">OK, la liste produits dans le panier</response>
        /// <response code="400">Requête invalide</response>
        /// <response code="401">Client non connecté</response>
        /// <response code="500">Exception non gérée</response>
        [HttpPost("{cacheMode:required}")]
        [ProducesResponseType(typeof(IEnumerable<PanierItem>), Status200OK)]
        [ProducesResponseType(typeof(void), Status400BadRequest)]
        [ProducesResponseType(typeof(void), Status401Unauthorized)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public async Task<IEnumerable<PanierItem>> AppendPanierItem(CacheMode cacheMode, PanierItem panierItem)
        {
            return await iPanierManagers[cacheMode].Append(panierItem);
        }
    }
}

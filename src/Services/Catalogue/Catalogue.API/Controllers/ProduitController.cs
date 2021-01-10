using Catalogue.API.Infrastructure;
using Catalogue.API.Models;
using Catalogue.API.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Catalogue.API.Controllers
{
    public class ProduitController : CommonController
    {
        private readonly IProduitFetcher iProduitFetcher;

        public ProduitController(IProduitFetcher iProduitFetcher)
        {
            this.iProduitFetcher = iProduitFetcher;
        }

        /// <summary>
        /// Retourne la liste des produits regroupé par famille
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:37001/produit'
        /// </remarks>
        /// <response code="200">OK, les produits groupés par famille</response>
        /// <response code="500">Exception non gérée</response>
        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, ICollection<Produit>>), Status200OK)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public async Task<Dictionary<string, ICollection<Produit>>> GetGroupByFamilles()
        {
            return await iProduitFetcher.GroupByFamilles();
        }
    }
}

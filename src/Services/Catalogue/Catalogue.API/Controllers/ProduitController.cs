using Catalogue.API.Infrastructure;
using Catalogue.API.Models;
using Catalogue.API.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogue.API.Controllers
{
    public class ProduitController : CommonController
    {
        private readonly IProduitFetcher iProduitFetcher;

        public ProduitController(IProduitFetcher iProduitFetcher)
        {
            this.iProduitFetcher = iProduitFetcher;
        }

        [HttpGet]
        public async Task<IDictionary<Famille, IEnumerable<Produit>>> Get()
        {
            return await iProduitFetcher.GroupByFamilles();
        }
    }
}

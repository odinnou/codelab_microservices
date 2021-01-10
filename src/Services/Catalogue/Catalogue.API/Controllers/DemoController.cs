using Catalogue.API.Configuration;
using Catalogue.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Catalogue.API.Controllers
{
    public class DemoController : CommonController
    {
        private readonly AppSettings appSettings;

        public DemoController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Retourne la configuration de l'API, surchargé ou non par les variables d'env ou le profile
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:37001/demo'
        /// </remarks>
        /// <response code="200">OK, la configuration de l'API</response>
        /// <response code="500">Exception non gérée</response>
        [HttpGet]
        [ProducesResponseType(typeof(AppSettings), Status200OK)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public AppSettings GetConfiguration()
        {
            return appSettings;
        }
    }
}

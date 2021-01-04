using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panier.API.Infrastructure;

namespace Panier.API.Controllers
{
    [Route("check-access")]
    public class CheckAccessController : CommonController
    {
        /*
         * curl -X GET 'http://localhost:{port}/check-access/panier-admin' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
         */
        [Authorize(ONLY_PANIER_ADMIN_POLICY)]
        [HttpGet("panier-admin")]
        public IActionResult OnlyPanierAdmin()
        {
            return NoContent();
        }

        /*
         * curl -X GET 'http://localhost:{port}/check-access/client' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
         */
        [Authorize(ONLY_CLIENT_POLICY)]
        [HttpGet("client")]
        public IActionResult OnlyClient()
        {
            return NoContent();
        }

        /*
         * curl -X GET 'http://localhost:{port}/check-access/tout-le-monde'
         */
        [AllowAnonymous] // Bypass toutes les validations
        [HttpGet("tout-le-monde")]
        public IActionResult All()
        {
            return NoContent();
        }
    }
}

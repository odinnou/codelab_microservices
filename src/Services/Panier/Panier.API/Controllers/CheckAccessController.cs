using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panier.API.Infrastructure;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Panier.API.Controllers
{
    [Route("check-access")]
    public class CheckAccessController : CommonController
    {
        /// <summary>
        /// Vérifie l'accès aux admin panier uniquement
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:{port}/check-access/panier-admin' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
        /// </remarks>
        /// <response code="204">OK, accès autorisé</response>
        /// <response code="500">Exception non gérée</response>
        [Authorize(ONLY_PANIER_ADMIN_POLICY)]
        [HttpGet("panier-admin")]
        [ProducesResponseType(typeof(void), Status204NoContent)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public IActionResult OnlyPanierAdmin()
        {
            return NoContent();
        }

        /// <summary>
        /// Vérifie l'accès aux clients uniquement
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:{port}/check-access/client' -H 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtp..........'
        /// </remarks>
        /// <response code="204">OK, accès autorisé</response>
        /// <response code="500">Exception non gérée</response>
        [Authorize(ONLY_CLIENT_POLICY)]
        [HttpGet("client")]
        [ProducesResponseType(typeof(void), Status204NoContent)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public IActionResult OnlyClient()
        {
            return NoContent();
        }

        /// <summary>
        /// Vérifie l'accès à tout le monde
        /// </summary>
        /// <remarks>
        /// curl -X GET 'http://localhost:{port}/check-access/tout-le-monde'
        /// </remarks>
        /// <response code="204">OK, accès autorisé</response>
        /// <response code="500">Exception non gérée</response>
        [AllowAnonymous] // Bypass toutes les validations
        [HttpGet("tout-le-monde")]
        [ProducesResponseType(typeof(void), Status204NoContent)]
        [ProducesResponseType(typeof(void), Status500InternalServerError)]
        public IActionResult All()
        {
            return NoContent();
        }
    }
}

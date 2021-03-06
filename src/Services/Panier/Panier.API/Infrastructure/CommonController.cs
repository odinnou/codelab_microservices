using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Panier.API.Infrastructure
{
    [Authorize] // DefaultPolicy : email vérifié obligatoirement
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public abstract class CommonController : ControllerBase
    {
        public const string ONLY_PANIER_ADMIN_POLICY = "ONLY_PANIER_ADMIN";
        public const string ONLY_CLIENT_POLICY = "ONLY_CLIENT";
    }
}

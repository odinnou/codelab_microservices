using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Catalogue.API.Infrastructure
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public abstract class CommonController : ControllerBase
    {
    }
}

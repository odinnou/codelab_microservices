using Catalogue.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace Catalogue.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class DemoController : ControllerBase
    {
        private readonly AppSettings appSettings;

        public DemoController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetConfiguration()
        {
            return new OkObjectResult(appSettings);
        }
    }
}

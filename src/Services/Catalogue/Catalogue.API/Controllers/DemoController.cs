using Catalogue.API.Configuration;
using Catalogue.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Catalogue.API.Controllers
{
    public class DemoController : CommonController
    {
        private readonly AppSettings appSettings;

        public DemoController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        [HttpGet]
        public AppSettings GetConfiguration()
        {
            return appSettings;
        }
    }
}

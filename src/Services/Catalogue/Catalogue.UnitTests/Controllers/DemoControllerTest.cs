using AutoFixture;
using Catalogue.API.Configuration;
using Catalogue.API.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Catalogue.UnitTests.Controllers
{
    public class DemoControllerTest
    {
        private readonly DemoController demoController;
        private readonly AppSettings appSettings;

        public DemoControllerTest()
        {
            appSettings = new Fixture().Create<AppSettings>();

            demoController = new DemoController(Options.Create(appSettings));
        }

        #region GetConfiguration

        [Fact]
        public void GetConfiguration_doit_retourner_la_configuration_de_lapplication()
        {
            //act
            AppSettings result = demoController.GetConfiguration();

            //assert
            result.Should().Be(appSettings);
        }

        #endregion
    }
}

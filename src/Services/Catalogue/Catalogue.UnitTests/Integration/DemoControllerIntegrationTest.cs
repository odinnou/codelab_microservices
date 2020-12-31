using Catalogue.API.Configuration;
using Catalogue.UnitTests.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Baker.UnitTests.Integration
{
    public class DemoControllerIntegrationTest
    {
        #region GetConfiguration

        [Fact]
        public async Task GetConfiguration_doit_renvoyer_200_et_retourner_la_configuration_de_lapplication_lorsque_tout_est_ok()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.ValidHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/demo");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            AppSettings result = JsonConvert.DeserializeObject<AppSettings>(httpResponse.Content.ReadAsStringAsync().Result);
            result.DbConnection.Should().Be("db intégration");
            result.ApiKeys.Should().HaveCount(2);
            result.ComplexItems.Should().NotBeEmpty();
        }

        #endregion
    }
}

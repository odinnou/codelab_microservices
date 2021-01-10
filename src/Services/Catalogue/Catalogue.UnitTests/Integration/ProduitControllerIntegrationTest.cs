using Catalogue.API.Models;
using Catalogue.UnitTests.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Catalogue.UnitTests.Integration
{
    public class ProduitControllerIntegrationTest : CatalogueIntegrationTest
    {
        #region GetGroupByFamilles

        [Fact]
        public async Task GetGroupByFamilles_doit_renvoyer_200_et_retourner_la_liste_des_familles_avec_les_produits_associés()
        {
            // arrange
            using (TestServer = new TestServer(HostConfiguration.ValidHostBuilder()))
            {
                ResetDatabase(Dataset.GroupByFamilles);
                HttpClient httpClient = TestServer.CreateClient();

                //act
                HttpResponseMessage httpResponse = await httpClient.GetAsync("/produit");

                //assert
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                Dictionary<string, ICollection<Produit>> result = JsonConvert.DeserializeObject<Dictionary<string, ICollection<Produit>>>(httpResponse.Content.ReadAsStringAsync().Result);
                result.Should().HaveCount(12);
                result["Chaussures Homme"].Should().NotBeEmpty();
            }
        }

        #endregion
    }
}

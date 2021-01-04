using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Panier.UnitTests.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Panier.UnitTests.Integration
{
    public class CheckAccessControllerIntegrationTest
    {
        #region OnlyPanierAdmin

        [Fact]
        public async Task OnlyPanierAdmin_doit_retourner_une_401_lorsque_aucune_information_dauthentification_nest_transmis()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AnonymousHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/panier-admin");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task OnlyPanierAdmin_doit_retourner_une_403_lorsque_lutilisateur_na_pas_validé_son_email()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder(emailVerified: false));
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/panier-admin");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task OnlyPanierAdmin_doit_retourner_une_403_lorsque_lutilisateur_est_un_client_avec_email_validé()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/panier-admin");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task OnlyPanierAdmin_doit_retourner_une_403_lorsque_ladmin_a_validé_son_email_mais_nest_pas_admin_panier()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder(type: "admin"));
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/panier-admin");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task OnlyPanierAdmin_doit_retourner_une_204_lorsque_ladmin_a_validé_son_email_et_est_admin_panier()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder(type: "admin", uid: "U2vCQ94KJTYBeHSUisSKLgM4mdW2"));
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/panier-admin");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region OnlyClient

        [Fact]
        public async Task OnlyClient_doit_retourner_une_401_lorsque_aucune_information_dauthentification_nest_transmis()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AnonymousHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/client");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task OnlyClient_doit_retourner_une_403_lorsque_lutilisateur_na_pas_validé_son_email()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder(emailVerified: false));
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/client");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task OnlyClient_doit_retourner_une_403_lorsque_lutilisateur_est_un_admin_avec_email_validé()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder(type: "admin"));
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/client");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task OnlyClient_doit_retourner_une_204_lorsque_le_client_a_validé_son_email()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AuthenticatedHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/client");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region All

        [Fact]
        public async Task All_doit_retourner_une_204_lorsque_aucune_information_dauthentification_nest_transmis()
        {
            // arrange
            using TestServer testServer = new TestServer(HostConfiguration.AnonymousHostBuilder());
            HttpClient httpClient = testServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/check-access/tout-le-monde");

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion
    }
}

using AutoFixture.Xunit2;
using Catalogue.API;
using Catalogue.API.Grpc;
using Catalogue.API.Models;
using Catalogue.API.UseCases;
using Catalogue.UnitTests.Configuration;
using FluentAssertions;
using Grpc.Core;
using Grpc.Core.Testing;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalogue.UnitTests.Grpc
{
    public class ProduitServiceTest
    {
        private readonly ProduitService produitService;
        private readonly Mock<IProduitFetcher> iProduitFetcher;


        public ProduitServiceTest()
        {
            iProduitFetcher = new Mock<IProduitFetcher>();

            produitService = new ProduitService(iProduitFetcher.Object);
        }

        #region GetUserWithFacebookId

        [Theory, DefaultAutoData]
        public async Task CheckDisponibilityByReference_doit_retourner_la_valeur_de_disponibilité_du_produit_correspondant(Produit produit, CheckDisponibilityRequest request)
        {
            // arrange
            ServerCallContext context = TestServerCallContext.Create("get", null, DateTime.MaxValue, null, System.Threading.CancellationToken.None, null, null, null, null, null, null);
            iProduitFetcher.Setup(mock => mock.GetByReference(request.Reference)).Returns(Task.FromResult(produit));

            // act
            CheckDisponibilityResponse result = await produitService.CheckDisponibilityByReference(request, context);

            // assert
            result.IsAvailable.Should().Be(produit.IsAvailable);
            context.Status.StatusCode.Should().Be(StatusCode.OK);
        }

        [Theory, AutoData]
        public void CheckDisponibilityByReference_doit_lever_la_même_exception_que_le_use_case_sil_en_leve_une(CheckDisponibilityRequest request, NotImplementedException exception)
        {
            // arrange
            ServerCallContext context = TestServerCallContext.Create("get", null, DateTime.MaxValue, null, System.Threading.CancellationToken.None, null, null, null, null, null, null);
            iProduitFetcher.Setup(mock => mock.GetByReference(request.Reference)).Throws(exception);

            // act
            Func<Task> result = async () => { await produitService.CheckDisponibilityByReference(request, context); };

            // assert
            result.Should().Throw<NotImplementedException>().WithMessage(exception.Message);
        }

        #endregion
    }
}

using AutoFixture.Xunit2;
using Catalogue.API;
using FluentAssertions;
using Grpc.Core;
using Grpc.Core.Testing;
using Moq;
using Panier.API.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Catalogue.API.ProduitProducer;

namespace Panier.UnitTests.Repositories
{
    public class CatalogueApiConsumerTest
    {
        private readonly Mock<ProduitProducerClient> produitProducerClient;
        private readonly ICatalogueApiConsumer iCatalogueApiConsumer;

        public CatalogueApiConsumerTest()
        {
            produitProducerClient = new Mock<ProduitProducerClient>();

            iCatalogueApiConsumer = new CatalogueApiConsumer(produitProducerClient.Object);
        }

        #region CheckDisponibility

        [Theory, AutoData]
        public async Task CheckDisponibility_doit_retourner_vrai_si_le_produit_est_disponible(string reference)
        {
            // arrange
            CheckDisponibilityResponse response = new CheckDisponibilityResponse { IsAvailable = true };
            AsyncUnaryCall<CheckDisponibilityResponse> encapsuledResponse = TestCalls.AsyncUnaryCall(Task.FromResult(response), Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });
            produitProducerClient.Setup(m => m.CheckDisponibilityByReferenceAsync(It.Is<CheckDisponibilityRequest>(request => request.Reference == reference), null, null, CancellationToken.None)).Returns(encapsuledResponse);

            // act
            bool result = await iCatalogueApiConsumer.CheckDisponibility(reference);

            // assert
            result.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task CheckDisponibility_doit_retourner_false_si_lappel_grpc_throw_une_Exception(string reference, NotImplementedException notImplementedException)
        {
            // arrange
            produitProducerClient.Setup(m => m.CheckDisponibilityByReferenceAsync(It.Is<CheckDisponibilityRequest>(request => request.Reference == reference), null, null, CancellationToken.None)).Throws(notImplementedException);

            // act
            bool result = await iCatalogueApiConsumer.CheckDisponibility(reference);

            // assert
            result.Should().BeFalse();
        }

        #endregion
    }
}

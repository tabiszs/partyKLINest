using Moq;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.ClientFacadeTests
{
    public class DeleteAccount
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task ThrowsClientNotFoundExceptionWhenThereIsNoClientWithGivenId()
        {
            var clientBuilder = new ClientBuilder();
            Client? returnedClient = null;
            long clientId = clientBuilder.TestId;

            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            await Assert.ThrowsAsync<ClientNotFoundException>(() => clientFacade.DeleteAccountAsync(clientId));
        }

        [Fact]
        public async Task DeletesAccount()
        {
            var clientBuilder = new ClientBuilder();
            Client? returnedClient = clientBuilder.Build();
            long clientId = clientBuilder.TestId;

            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            await clientFacade.DeleteAccountAsync(clientId);

            _mockClientRepo
                .Verify(x => x.DeleteAsync(returnedClient, default), Times.Once);
        }
    }
}

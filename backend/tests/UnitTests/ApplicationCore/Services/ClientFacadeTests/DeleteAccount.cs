using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
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
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsClientNotFoundExceptionWhenThereIsNoClientWithGivenId()
        {
            var clientBuilder = new ClientBuilder();
            Client? returnedClient = null;
            string clientId = clientBuilder.TestId;

            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedClient);

            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await Assert.ThrowsAsync<ClientNotFoundException>(() => clientFacade.DeleteAccountAsync(clientId));
        }

        [Fact]
        public async Task DeletesAccount()
        {
            var clientBuilder = new ClientBuilder();
            Client? returnedClient = clientBuilder.Build();
            string clientId = clientBuilder.TestId;

            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedClient);

            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await clientFacade.DeleteAccountAsync(clientId);

            _mockClientRepo
                .Verify(x => x.DeleteAsync(returnedClient, default), Times.Once);
        }
    }
}

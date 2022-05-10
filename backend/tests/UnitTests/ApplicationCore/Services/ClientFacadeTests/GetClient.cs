using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class GetClient
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoClientWithGivenId()
        {
            Client? returnedClient = null;
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(returnedClient);

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await Assert.ThrowsAsync<ClientNotFoundException>(() => clientFacade.GetClientAsync("1"));
        }

        [Fact]
        public async Task ReturnsClientIfItExistsInRepo()
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.GetWithDefaultValues();
            Client? expected = clientBuilder.Build();

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(expected);

            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            var result = await clientFacade.GetClientAsync(clientBuilder.TestId);
            Assert.Equal(expected, result);
        }
    }
}
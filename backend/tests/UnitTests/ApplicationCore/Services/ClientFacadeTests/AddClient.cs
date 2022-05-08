using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class AddClient
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task AddsNewClient()
        {

            var clientBuilder = new ClientBuilder();
            Client client = clientBuilder.Build();

            _mockClientRepo
                .Setup(x => x.AddAsync(It.IsAny<Client>(), default))
                .ReturnsAsync(client);

            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await clientFacade.AddClientAsync(client);

            _mockClientRepo
                .Verify(x => x.AddAsync(It.Is<Client>(o => o == client), default), Times.Once);
        }
    }
}
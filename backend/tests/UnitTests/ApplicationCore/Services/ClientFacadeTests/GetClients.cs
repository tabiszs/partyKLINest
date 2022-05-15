using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class GetClients
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ReturnsAllClients()
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.GetWithDefaultValues();
            Client expectedClient1 = clientBuilder.Build();
            Client expectedClient2 = clientBuilder.Build();
            Client expectedClient3 = clientBuilder.Build();
            var expected = new List<Client>()
            {
                expectedClient1,
                expectedClient2,
                expectedClient3,
            };

            _mockClientRepo.Setup(x => x.ListAsync(default)).ReturnsAsync(expected);

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);
            var results = await clientFacade.GetClientsAsync();

            Assert.Equal(expected, results);
        }
    }
}
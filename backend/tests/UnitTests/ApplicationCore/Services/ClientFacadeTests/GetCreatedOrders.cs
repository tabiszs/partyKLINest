using Ardalis.Specification;
using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class GetCreatedOrders
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsClientNotFoundExceptionWhenThereIsNoClientWithGivenId()
        {
            Client? returnedClient = null;
            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedClient);

            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await Assert.ThrowsAsync<ClientNotFoundException>(
                () => clientFacade.GetCreatedOrdersAsync("xx123"));
        }

        [Fact]
        public async Task ReturnsOrdersWhichAreAssignedToGivenCleaner()
        {
            // Arrange
            var clientBuilder = new ClientBuilder();
            Client returnedClient = clientBuilder.Build();
            string clientId = returnedClient.ClientId;
            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedClient);

            var ordersBuilder = new OrderBuilder();
            List<Order> expected = ordersBuilder.GenerateOrdersWith(3, clientId);
            _mockOrderRepo
                .Setup(x => x.ListAsync(It.IsAny<Specification<Order>>(), default))
                .ReturnsAsync(expected);

            // Act
            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);
            var results = await clientFacade.GetCreatedOrdersAsync(clientId);

            // Assert
            Assert.Equal(expected, results);
        }
    }
}

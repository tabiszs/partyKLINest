using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.ApplicationCore.Specifications;
using System.Collections.Generic;
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

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await Assert.ThrowsAsync<ClientNotFoundException>(() => clientFacade.DeleteClientAsync(clientId));
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

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await clientFacade.DeleteClientAsync(clientId);

            _mockClientRepo
                .Verify(x => x.DeleteAsync(returnedClient, default), Times.Once);
        }

        [Fact]
        public async Task DeletesOrders()
        {
            var clientBuilder = new ClientBuilder();
            Client? returnedClient = clientBuilder.Build();
            string clientId = clientBuilder.TestId;

            var orderBuilder = new OrderBuilder();
            orderBuilder.TestClientId = clientId;
            var orderToDelete1 = orderBuilder.Build();
            var orderToDelete2 = orderBuilder.Build();

            var ordersToDelete = new List<Order>()
            {
                orderToDelete1,
                orderToDelete2,
            };

            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedClient);

            _mockOrderRepo.Setup(x => x.ListAsync(It.IsAny<OrdersCreatedBySpecification>(), default)).ReturnsAsync(ordersToDelete);

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            await clientFacade.DeleteClientAsync(clientId);

            _mockOrderRepo
                .Verify(x => x.DeleteRangeAsync(ordersToDelete, default), Times.Once);
        }
    }
}
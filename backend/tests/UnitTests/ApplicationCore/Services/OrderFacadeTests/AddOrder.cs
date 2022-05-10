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
    public class AddOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task AddsOrderToRepo()
        {
            var orderBuilder = new OrderBuilder();
            Order order = orderBuilder.Build();

            _mockOrderRepo
                .Setup(x => x.AddAsync(It.IsAny<Order>(), default))
                .ReturnsAsync(order);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await orderFacade.AddOrderAsync(order);

            _mockOrderRepo
                .Verify(x => x.AddAsync(It.Is<Order>(o => o == order), default), Times.Once);
        }

        [Fact]
        public async Task AddsClientIfItDoesntExistInDb()
        {
            var orderBuilder = new OrderBuilder();
            Order order = orderBuilder.Build();

            _mockOrderRepo
                .Setup(x => x.AddAsync(It.IsAny<Order>(), default))
                .ReturnsAsync(order);
            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync((Client?)null);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await orderFacade.AddOrderAsync(order);

            _mockClientRepo
                .Verify(x => x.AddAsync(It.Is<Client>(c => c.ClientId == order.ClientId), default), Times.Once);
        }

        [Fact]
        public async Task DoesntAddClientIfItExistsInDb()
        {
            var orderBuilder = new OrderBuilder();
            Order order = orderBuilder.Build();

            _mockOrderRepo
                .Setup(x => x.AddAsync(It.IsAny<Order>(), default))
                .ReturnsAsync(order);
            _mockClientRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(new Client(order.ClientId));

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await orderFacade.AddOrderAsync(order);

            _mockClientRepo
                .Verify(x => x.AddAsync(It.IsAny<Client>(), default), Times.Never);
        }
    }
}

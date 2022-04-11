using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
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

        [Fact]
        public async Task AddsOrderToRepo()
        {
            var orderBuilder = new OrderBuilder();
            Order order = orderBuilder.Build();

            _mockOrderRepo
                .Setup(x => x.AddAsync(It.IsAny<Order>(), default))
                .ReturnsAsync(order);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            await orderFacade.AddOrderAsync(order);

            _mockOrderRepo
                .Verify(x => x.AddAsync(It.Is<Order>(o => o == order), default), Times.Once);
        }
    }
}

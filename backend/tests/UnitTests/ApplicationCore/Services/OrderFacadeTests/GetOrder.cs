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
    public class GetOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            Order? returnedOrder = null;
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await Assert.ThrowsAsync<OrderNotFoundException>(() => orderFacade.GetOrderAsync(1));
        }

        [Fact]
        public async Task ReturnsOrderIfItExistsInRepo()
        {
            var orderBuilder = new OrderBuilder(new AddressFactory());
            orderBuilder.WithDefaultValues();
            Order? expected = orderBuilder.Build();

            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(expected);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            var result = await orderFacade.GetOrderAsync(orderBuilder.TestOrderId);
            Assert.Equal(expected, result);
        }
    }
}

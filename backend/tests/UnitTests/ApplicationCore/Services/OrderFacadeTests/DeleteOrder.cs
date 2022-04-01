using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class DeleteOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            var orderBuilder = new OrderBuilder();
            Order? returnedOrder = null;
            long orderId = orderBuilder.TestOrderId;

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            await Assert.ThrowsAsync<OrderNotFoundException>(() => orderFacade.DeleteOrderAsync(orderId));
        }

        [Fact]
        public async Task DeletesOrderFromRepo()
        {
            var orderBuilder = new OrderBuilder();
            Order? returnedOrder = orderBuilder.Build();
            long orderId = orderBuilder.TestOrderId;

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            await orderFacade.DeleteOrderAsync(orderId);

            _mockOrderRepo
                .Verify(x => x.DeleteAsync(returnedOrder, default), Times.Once);
        }
    }
}

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
    public class CancellOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();


        [Theory(DisplayName = "Throw exception when status is:")]
        [InlineData(OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Closed)]
        public async Task ThrowsNotCorrectOrderStatusExceptionWhenStatusIsNotCorrect(OrderStatus status)
        {
            // Arrange
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithStaus(status);
            var order = orderBuilder.Build();

            // Act
            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            // Assert
            await Assert.ThrowsAsync<NotCorrectOrderStatusException>(() => orderFacade.CancelOrderAsync(order));
        }

        [Theory(DisplayName = "Cancell orders when status is:")]
        [InlineData(OrderStatus.Active)]
        [InlineData(OrderStatus.InProgress)]
        public async Task DeletesOrderFromRepo(OrderStatus status)
        {
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithStaus(status);
            var order = orderBuilder.Build();

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            await orderFacade.CancelOrderAsync(order);

            _mockOrderRepo
                .Verify(x => x.UpdateAsync(order, default), Times.Once);
        }
    }
}

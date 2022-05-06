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
    public class SubmitOpinionCleaner
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            var orderBuilder = new OrderBuilder();
            Order? returnedOrder = null;
            var opinion = orderBuilder.TestOpinion;
            long orderId = orderBuilder.TestOrderId;

            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await Assert.ThrowsAsync<OrderNotFoundException>(() => orderFacade.SubmitOpinionCleanerAsync(orderId, opinion));
        }

        [Fact]
        public async Task AddsCleanersOpinionToOrder()
        {
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId();
            var order = orderBuilder.Build();
            var opinion = orderBuilder.TestOpinion;
            var orderId = orderBuilder.TestOrderId;

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(order);

            Assert.Null(order.CleanersOpinion);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);
            await orderFacade.SubmitOpinionCleanerAsync(orderId, opinion);

            _mockOrderRepo.Verify(x =>
                x.UpdateAsync(
                    It.Is<Order>(o => o.CleanersOpinion != null && o.CleanersOpinion == opinion),
                    default),
                Times.Once
                );
        }
    }
}

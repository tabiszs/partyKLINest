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
    public class SubmitOpinionClient
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            var orderBuilder = new OrderBuilder();
            Order? returnedOrder = null;
            var opinion = orderBuilder.TestOpinion;
            long orderId = orderBuilder.TestOrderId;

            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);

            await Assert.ThrowsAsync<OrderNotFoundException>(() => orderFacade.SubmitOpinionClientAsync(orderId, opinion));
        }

        [Fact]
        public async Task AddsClientsOpinionToOrder()
        {
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId();
            var order = orderBuilder.Build();
            var opinion = orderBuilder.TestOpinion;
            var orderId = orderBuilder.TestOrderId;

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(order);

            Assert.Null(order.ClientsOpinion);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object, _mockClientRepo.Object);
            await orderFacade.SubmitOpinionClientAsync(orderId, opinion);

            _mockOrderRepo.Verify(x =>
                x.UpdateAsync(
                    It.Is<Order>(o => o.ClientsOpinion != null && o.ClientsOpinion == opinion),
                    default),
                Times.Once
                );
        }
    }
}

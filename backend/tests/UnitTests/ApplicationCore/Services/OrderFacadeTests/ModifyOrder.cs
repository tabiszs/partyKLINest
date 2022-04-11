using Moq;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class ModifyOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            Order? returnedOrder = null;
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            string newCleanerId = "newCleanerId";
            string newClientId = "newCustomerId";
            OrderStatus newOrderStatus = OrderStatus.InProgress;
            decimal newMaxPrice = 100;
            int newRating = 5;
            DateTimeOffset newDate = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            Address newAddress = new AddressFactory().CreateWithFlatNumber();
            MessLevel newMessLevel = MessLevel.Disaster;

            await Assert.ThrowsAsync<OrderNotFoundException>(() =>
                orderFacade.ModifyOrderAsync(1, newClientId, newCleanerId,
                newOrderStatus, newMaxPrice, newRating, newDate, newAddress,
                newMessLevel));
        }

        [Fact]
        public async Task GetsOrderFromRepo()
        {
            Order? returnedOrder = new OrderBuilder().Build();
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            string newCleanerId = "newCleanerId";
            string newClientId = "newCustomerId";
            OrderStatus newOrderStatus = OrderStatus.InProgress;
            decimal newMaxPrice = 100;
            int newRating = 5;
            DateTimeOffset newDate = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            Address newAddress = new AddressFactory().CreateWithFlatNumber();
            MessLevel newMessLevel = MessLevel.Disaster;

            await orderFacade.ModifyOrderAsync(1, newClientId, newCleanerId,
                newOrderStatus, newMaxPrice, newRating, newDate, newAddress,
                newMessLevel);

            _mockOrderRepo.Verify(x => x.GetByIdAsync(It.IsAny<long>(), default), Times.Once);
        }

        [Fact]
        public async Task SavesNewOrderToRepo()
        {
            Order? returnedOrder = new OrderBuilder().Build();
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var orderFacade = new OrderFacade(_mockOrderRepo.Object);

            string newCleanerId = "newCleanerId";
            string newClientId = "newCustomerId";
            OrderStatus newOrderStatus = OrderStatus.InProgress;
            decimal newMaxPrice = 100;
            int newRating = 5;
            DateTimeOffset newDate = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            Address newAddress = new AddressFactory().CreateWithFlatNumber();
            MessLevel newMessLevel = MessLevel.Disaster;

            await orderFacade.ModifyOrderAsync(1, newClientId, newCleanerId,
                newOrderStatus, newMaxPrice, newRating, newDate, newAddress,
                newMessLevel);

            _mockOrderRepo.Verify(x => x.UpdateAsync(It.IsAny<Order>(), default), Times.Once);
        }
    }
}

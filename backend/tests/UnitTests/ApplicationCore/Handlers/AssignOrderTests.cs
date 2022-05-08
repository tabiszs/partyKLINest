using Moq;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Handlers;
using PartyKlinest.ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;


namespace UnitTests.ApplicationCore.Handlers
{
    public  class AssignOrderTests
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<AssignOrder> _mockAssignOrderHandler = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            Order? returnedOrder = null;
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var assignHandler = new AssignOrder(
                _mockCleanerRepo.Object, 
                _mockClientRepo.Object,
                _mockOrderRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerNotFoundException>(
                () => assignHandler.AssignOrderToCleaner(1, "1"));
        }

        [Fact]
        public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            Cleaner? returnedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            Order? returnedOrder = null;
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var assignHandler = new AssignOrder(
                _mockCleanerRepo.Object,
                _mockClientRepo.Object,
                _mockOrderRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotFoundException>(
                () => assignHandler.AssignOrderToCleaner(1, returnedCleaner.CleanerId));
        }

        [Theory(DisplayName ="Throw not active exception")]
        [InlineData(OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Closed)]
        [InlineData(OrderStatus.InProgress)]
        public async Task OrderNotActiveException(OrderStatus orderStatus)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            Cleaner? returnedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithStaus(orderStatus);
            Order returnedOrder = orderBuilder.Build();
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            var assignHandler = new AssignOrder(
                _mockCleanerRepo.Object,
                _mockClientRepo.Object,
                _mockOrderRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotActiveException>(
                () => assignHandler.AssignOrderToCleaner(returnedOrder.OrderId, returnedCleaner.CleanerId));

        }

        [Theory(DisplayName = "Assign when Order is in Active Status")]
        [InlineData(OrderStatus.Active)]
        public async Task AssignOrdersTest(OrderStatus orderStatus)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            Cleaner? returnedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            var orderBuilder = new OrderBuilder();
            orderBuilder.WithStaus(orderStatus);
            Order? returnedOrder = orderBuilder.Build();
            _mockOrderRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), default)).ReturnsAsync(returnedOrder);

            AssignOrder assignOrder = new(_mockCleanerRepo.Object, _mockClientRepo.Object, _mockOrderRepo.Object);

            await assignOrder.AssignOrderToCleaner(returnedOrder.OrderId, returnedCleaner.CleanerId);

            _mockOrderRepo.Verify(x => x.UpdateAsync(It.IsAny<Order>(), default), Times.Once);
        }

    }
}

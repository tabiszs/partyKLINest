using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class AcceptRejectOrder
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();

        [Theory(DisplayName = "Accept Order")]
        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.InProgress, "localCleanerId22")]
        public async Task AcceptOrder(
            string personalCleanerId, OrderStatus localStatus, string localCleanerId, OrderStatus sentStatus, string? sentCleanerId)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithCleanerId(personalCleanerId);
            var localCleaner = cleanerBuilder.Build();
            localCleaner.SetCleanerStatus(CleanerStatus.Active);

            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(localCleanerId);
            orderBuilder.WithStaus(localStatus);
            var localOrder = orderBuilder.Build();

            orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(sentCleanerId);
            orderBuilder.WithStaus(sentStatus);
            var sentOrder = orderBuilder.Build();

            var cleanerFacade = SetMockRepos(localCleaner, localOrder);

            // Act
            await cleanerFacade.AcceptRejectOrder(personalCleanerId, sentOrder);

            // Assert
            _mockOrderRepo.Verify(x => x.UpdateAsync(It.IsAny<Order>(), default), Times.Once);
        }

        [Theory(DisplayName = "Reject Order")]
        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Active, null)]
        public async Task RejectOrder(
            string personalCleanerId, OrderStatus localStatus, string localCleanerId, OrderStatus sentStatus, string? sentCleanerId)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithCleanerId(personalCleanerId);
            var localCleaner = cleanerBuilder.Build();
            localCleaner.SetCleanerStatus(CleanerStatus.Active);

            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(localCleanerId);
            orderBuilder.WithStaus(localStatus);
            var localOrder = orderBuilder.Build();

            orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(sentCleanerId);
            orderBuilder.WithStaus(sentStatus);
            var sentOrder = orderBuilder.Build();

            var cleanerFacade = SetMockRepos(localCleaner, localOrder);

            // Act
            await cleanerFacade.AcceptRejectOrder(personalCleanerId, sentOrder);

            // Assert
            _mockOrderRepo.Verify(x => x.UpdateAsync(It.IsAny<Order>(), default), Times.Once);
        }

        [Theory(DisplayName = "Errors when change order status")]
        // accept [InlineData("localCleanerId22", OrderStatus.Active,     "localCleanerId22", OrderStatus.InProgress, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.InProgress, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.InProgress, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.InProgress, "localCleanerId22")]

        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Active, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Active, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Active, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Active, "localCleanerId22")]

        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Closed, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Closed, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Closed, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Closed, "localCleanerId22")]

        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Cancelled, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Cancelled, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Cancelled, "localCleanerId22")]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Cancelled, "localCleanerId22")]

        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.InProgress, null)]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.InProgress, null)]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.InProgress, null)]

        // reject [InlineData("localCleanerId22", OrderStatus.Active,     "localCleanerId22", OrderStatus.Active, null)]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Active, null)]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Active, null)]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Active, null)]

        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Closed, null)]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Closed, null)]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Closed, null)]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Closed, null)]

        [InlineData("localCleanerId22", OrderStatus.Active, "localCleanerId22", OrderStatus.Cancelled, null)]
        [InlineData("localCleanerId22", OrderStatus.Cancelled, "localCleanerId22", OrderStatus.Cancelled, null)]
        [InlineData("localCleanerId22", OrderStatus.Closed, "localCleanerId22", OrderStatus.Cancelled, null)]
        [InlineData("localCleanerId22", OrderStatus.InProgress, "localCleanerId22", OrderStatus.Cancelled, null)]
        public async Task ThrowsNotCorrectCurrentOrderStateExceptionWhileChangingToCloseState(
            string personalCleanerId, OrderStatus localStatus, string localCleanerId, OrderStatus sentStatus, string? sentCleanerId)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithCleanerId(personalCleanerId);
            var localCleaner = cleanerBuilder.Build();
            localCleaner.SetCleanerStatus(CleanerStatus.Active);

            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(localCleanerId);
            orderBuilder.WithStaus(localStatus);
            var localOrder = orderBuilder.Build();

            orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(sentCleanerId);
            orderBuilder.WithStaus(sentStatus);
            var sentOrder = orderBuilder.Build();

            var cleanerFacade = SetMockRepos(localCleaner, localOrder);

            // Act & Assert
            await Assert.ThrowsAsync<NotCorrectOrderStatusException>(
                () => cleanerFacade.AcceptRejectOrder(personalCleanerId, sentOrder));


        }

        private CleanerFacade SetMockRepos(Cleaner returnedCleaner, Order expected)
        {
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(expected);

            return new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);
        }
    }
}

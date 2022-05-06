using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class ConfirmOrderCompleted
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            var orderBuilder = new OrderBuilder();
            var order = orderBuilder.Build();
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerNotFoundException>(
                () => cleanerFacade.ConfirmOrderCompleted(
                    orderBuilder.TestCleanerId,
                    orderBuilder.TestOrderId,
                    orderBuilder.TestOpinion));
        }

        [Theory(DisplayName = "Not Acceptable previous OrderStatus while order changed as Closed")]
        [InlineData(OrderStatus.Active)]
        [InlineData(OrderStatus.Cancelled)]
        [InlineData(OrderStatus.Closed)]
        public async Task ThrowsNotCorrectCurrentOrderStateExceptionWhileChangingToCloseState(OrderStatus status)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            var returnedCleaner = cleanerBuilder.Build();
            returnedCleaner.SetCleanerStatus(CleanerStatus.Active);

            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId("it is not returnedCleaner Id");
            orderBuilder.WithStaus(status);

            var expected = orderBuilder.Build();
            var newOpinion = new Opinion(4, "New Opinion");
            var cleanerFacade = SetMockRepos(returnedCleaner, expected);

            // Act & Assert
            await Assert.ThrowsAsync<UserWithoutPrivilegesException>(
                () => cleanerFacade.ConfirmOrderCompleted(
                    returnedCleaner.CleanerId,
                    expected.OrderId,
                    newOpinion));
        }

        [Fact]
        public async Task VerifyUpdateOfOrder()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            var returnedCleaner = cleanerBuilder.Build();
            returnedCleaner.SetCleanerStatus(CleanerStatus.Active);

            var orderBuilder = new OrderBuilder();
            orderBuilder.WithCleanerId(returnedCleaner.CleanerId);
            orderBuilder.WithStaus(OrderStatus.InProgress);
            var expected = orderBuilder.Build();

            var newOpinion = new Opinion(4, "New Opinion");
            var cleanerFacade = SetMockRepos(returnedCleaner, expected);

            // Act
            await cleanerFacade.ConfirmOrderCompleted(
                    returnedCleaner.CleanerId,
                    expected.OrderId,
                    newOpinion);

            // Assert
            _mockOrderRepo.Verify(x => x.UpdateAsync(It.IsAny<Order>(), default), Times.Once);
        }

        private CleanerFacade SetMockRepos(Cleaner returnedCleaner, Order expected)
        {
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);

            _mockOrderRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(expected);

            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            return new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);
        }
    }
}

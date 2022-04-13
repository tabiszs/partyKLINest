using Moq;
using PartyKlinest.ApplicationCore.Entities;
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
    public class UpdateOrderFilter
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();

        [Theory]
        [InlineData(CleanerStatus.Active, CleanerStatus.Registered)]
        [InlineData(CleanerStatus.Registered, CleanerStatus.Registered)]
        public async Task ThrowsUserNotActiveExceptionn(CleanerStatus localStatus, CleanerStatus sentStatus)
        {
            // Arrange
            var orderFilter = new OrderFilter(MessLevel.Moderate, 1, 10);
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner localCleaner = cleanerBuilder.Build();

            orderFilter = new OrderFilter(MessLevel.Huge, 4, 100);
            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(localCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotActiveException>(
                () => cleanerFacade.UpdateCleanerAsync(sentCleaner));
        }

        [Theory(DisplayName = "Make 1 Update: orderFilter")]
        [InlineData(CleanerStatus.Active, CleanerStatus.Active)]
        public async Task VerifyUpdateOrderFilter1(CleanerStatus localStatus, CleanerStatus sentStatus)
        {
            // Arrange
            var orderFilter = new OrderFilter(MessLevel.Moderate, 1, 10);
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner localCleaner = cleanerBuilder.Build();

            orderFilter = new OrderFilter(MessLevel.Huge, 4, 100);
            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(localCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Once);
        }

        [Theory(DisplayName = "Make 2 Updates: status, orderFilter")]
        [InlineData(CleanerStatus.Registered, CleanerStatus.Active)]
        public async Task VerifyUpdateOrderFilter2(CleanerStatus localStatus, CleanerStatus sentStatus)
        {
            // Arrange
            var orderFilter = new OrderFilter(MessLevel.Moderate, 1, 10);
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner localCleaner = cleanerBuilder.Build();

            orderFilter = new OrderFilter(MessLevel.Huge, 4, 100);
            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            cleanerBuilder.WithOrderFilter(orderFilter);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(localCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Exactly(2));
        }
    }
}

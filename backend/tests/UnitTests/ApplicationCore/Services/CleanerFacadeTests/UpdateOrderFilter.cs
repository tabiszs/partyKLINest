using Moq;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
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
    public class UpdateOrderFilter
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IGraphClient> _mockGraphClient = new();

        [Theory]
        [InlineData(CleanerStatus.Active, CleanerStatus.Banned)]
        [InlineData(CleanerStatus.Banned, CleanerStatus.Banned)]
        public async Task BannedCleanerCannotAddNewEntry(CleanerStatus localStatus, CleanerStatus sentStatus)
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
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerCannotChangeBannedStatusException>(
                () => cleanerFacade.UpdateCleanerAsync(sentCleaner));
        }

        [Theory(DisplayName = "When new entry is sent. Registered Status is changed in Active")]
        [InlineData(CleanerStatus.Registered, CleanerStatus.Registered)]
        public async Task AddNewUpdateChangeAutomaticalyInActiveStatus(CleanerStatus localStatus, CleanerStatus sentStatus)
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
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Exactly(2));
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
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

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
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Exactly(2));
        }
    }
}

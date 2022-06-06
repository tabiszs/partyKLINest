using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.ApplicationCore.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class UpdateStatus
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();
        private readonly Mock<IGraphClient> _mockGraphClient = new();

        [Theory(DisplayName = "Cleaner cannot change status from banned to ")]
        [InlineData(CleanerStatus.Active)]
        [InlineData(CleanerStatus.Banned)]
        [InlineData(CleanerStatus.Registered)]
        public async Task ThrowsCleanerCannotChangeFromBannedStatusException(CleanerStatus sentStatus)
        {
            // Arrange
            const CleanerStatus localStatus = CleanerStatus.Banned;
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            Cleaner localCleaner = cleanerBuilder.Build();

            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.ListAsync(It.IsAny<CleanerWithSchedule>(), default))
                .ReturnsAsync(new List<Cleaner> { localCleaner });
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerCannotChangeBannedStatusException>(
                () => cleanerFacade.UpdateCleanerAsync(sentCleaner));
        }

        [Theory(DisplayName = "Cleaner cannot change status to banned from ")]
        [InlineData(CleanerStatus.Active)]
        [InlineData(CleanerStatus.Banned)]
        [InlineData(CleanerStatus.Registered)]
        public async Task ThrowsCleanerCannotChangeToBannedStatusException(CleanerStatus localStatus)
        {
            // Arrange
            const CleanerStatus sentStatus = CleanerStatus.Banned;
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            Cleaner localCleaner = cleanerBuilder.Build();

            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.ListAsync(It.IsAny<CleanerWithSchedule>(), default))
                .ReturnsAsync(new List<Cleaner> { localCleaner });
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerCannotChangeBannedStatusException>(
                () => cleanerFacade.UpdateCleanerAsync(sentCleaner));
        }

        [Theory]
        [InlineData(CleanerStatus.Active, CleanerStatus.Registered)]
        [InlineData(CleanerStatus.Registered, CleanerStatus.Active)]
        public async Task VerifyUpdateOfStatus(CleanerStatus localStatus, CleanerStatus sentStatus)
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(localStatus);
            Cleaner localCleaner = cleanerBuilder.Build();

            cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.WithStatus(sentStatus);
            Cleaner sentCleaner = cleanerBuilder.Build();

            _mockCleanerRepo
                .Setup(x => x.ListAsync(It.IsAny<CleanerWithSchedule>(), default))
                .ReturnsAsync(new List<Cleaner> { localCleaner });
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);

            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act
            await cleanerFacade.UpdateCleanerAsync(sentCleaner);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Once);
        }
    }
}

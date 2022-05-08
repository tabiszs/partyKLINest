using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class UnbanCleaner
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerNotFoundException>(
                () => cleanerFacade.BanAsync("xx123"));
        }

        [Fact]
        public async Task ThrowsCleanerIsNotBannedExceptionWhenCleanerIsNotBanned()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.status = CleanerStatus.Active;
            var expectedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(expectedCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerIsNotBannedException>(
                () => cleanerFacade.UnbanAsync(expectedCleaner.CleanerId));
        }

        [Fact]
        public async Task Ban()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            cleanerBuilder.status = CleanerStatus.Banned;
            var expectedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(expectedCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

            // Act
            await cleanerFacade.BanAsync(expectedCleaner.CleanerId);

            // Assert
            _mockCleanerRepo.Verify(x => x.UpdateAsync(It.IsAny<Cleaner>(), default), Times.Once);
        }
    }
}

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
    public class GetCleanerInfo
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerNotFoundException>(
                () => cleanerFacade.GetAssignedOrdersAsync("xx123"));
        }

        [Fact]
        public async Task GetCleanerWithGivenIdFromRepository()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            var expectedCleaner = cleanerBuilder.Build();
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(expectedCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

            // Act
            var returnedCleaner = await cleanerFacade.GetCleanerInfo(expectedCleaner.CleanerId);

            // Assert
            Assert.Equal(expectedCleaner, returnedCleaner);
        }
    }
}

using Ardalis.Specification;
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
    public class GetAssignedOrders
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IClientService> _mockClientService = new();
        private readonly Mock<IGraphClient> _mockGraphClient = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CleanerNotFoundException>(
                () => cleanerFacade.GetAssignedOrdersAsync("xx123"));
        }

        [Fact]
        public async Task ReturnsOrdersWhichAreAssignedToGivenCleaner()
        {
            // Arrange
            var cleanerBuilder = new CleanerBuilder();
            Cleaner returnedCleaner = cleanerBuilder.Build();
            string cleanerId = returnedCleaner.CleanerId;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(returnedCleaner);
            _mockCleanerRepo
                .Setup(x => x.ListAsync(It.IsAny<CleanerWithSchedule>(), default))
                .ReturnsAsync(new List<Cleaner> { returnedCleaner });

            var ordersBuilder = new OrderBuilder();
            List<Order> expected = ordersBuilder.GenerateOrdersWith(3, cleanerId);
            _mockOrderRepo
                .Setup(x => x.ListAsync(It.IsAny<Specification<Order>>(), default))
                .ReturnsAsync(expected);

            // Act
            OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object, _mockGraphClient.Object);

            var results = await cleanerFacade.GetAssignedOrdersAsync(cleanerId);

            // Assert
            Assert.Equal(expected, results);
        }
    }
}

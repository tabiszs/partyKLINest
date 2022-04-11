using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;
using Ardalis.Specification;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests
{
    public class GetAssignedOrders
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
        private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();

        [Fact]
        public async Task ThrowsCleanerNotFoundExceptionWhenThereIsNoCleanerWithGivenId()
        {
            // Arrange
            Cleaner? returnedCleaner = null;
            _mockCleanerRepo
                .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
                .ReturnsAsync(returnedCleaner);
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);

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

            var ordersBuilder = new OrderBuilder();
            List<Order> expected = ordersBuilder.GenerateOrdersWith(3, cleanerId);
            _mockOrderRepo
                .Setup(x => x.ListAsync(It.IsAny<Specification<Order>>(), default))
                .ReturnsAsync(expected);

            // Act
            var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, _mockOrderRepo.Object);
            var results = await cleanerFacade.GetAssignedOrdersAsync(cleanerId);

            // Assert
            Assert.Equal(expected, results);
        }
    }
}

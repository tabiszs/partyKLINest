using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.ApplicationCore.Specifications;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.CleanerFacadeTests;

public class ListCleanersMatchingOrder
{
    private readonly Mock<IRepository<Order>> _mockOrderRepo = new();
    private readonly Mock<IRepository<Cleaner>> _mockCleanerRepo = new();
    private readonly Mock<IRepository<Client>> _mockClientRepo = new();
    private readonly Mock<IClientService> _mockClientService = new();
    private readonly OrderBuilder _orderBuilder = new();

    [Fact]
    public async Task ThrowsOrderNotFoundExceptionWhenThereIsNoOrderWithGivenId()
    {
        Order? returnedOrder = null;

        // Arrange
        _mockOrderRepo
            .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
            .ReturnsAsync(returnedOrder);
        OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
        var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<OrderNotFoundException>(
            () => cleanerFacade.ListCleanersMatchingOrderAsync(12));
    }

    [Fact]
    public async Task UsesClientServiceToGetAverageRating()
    {
        _orderBuilder.WithDefaultValues();
        var order = _orderBuilder.Build();

        // Arrange
        _mockOrderRepo
            .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
            .ReturnsAsync(order);

        OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
        var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

        // Act 
        await cleanerFacade.ListCleanersMatchingOrderAsync(12);

        // Assert
        _mockClientService.Verify(x => x.GetAverageClientRatingAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UsesCleanerRepoWithMatchingSpecificationToGetCleanersMatchingOrder()
    {
        _orderBuilder.WithDefaultValues();
        var order = _orderBuilder.Build();

        // Arrange
        _mockOrderRepo
            .Setup(x => x.GetByIdAsync(It.IsAny<long>(), default))
            .ReturnsAsync(order);

        OrderFacade orderFacade = new(_mockOrderRepo.Object, _mockClientRepo.Object);
        var cleanerFacade = new CleanerFacade(_mockCleanerRepo.Object, orderFacade, _mockClientService.Object);

        // Act 
        await cleanerFacade.ListCleanersMatchingOrderAsync(12);

        // Assert
        _mockCleanerRepo.Verify(x => x.ListAsync(It.IsAny<CleanersMatchingOrderSpecification>(),
            default), Times.Once);
    }
}
using Moq;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.ClientFacadeTests
{
    public class BanClient
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();
        private readonly Mock<IRepository<Order>> _mockOrderRepo = new();


        [Fact]
        public async Task BansClient()
        {
            //Client? returnedClient = new ClientBuilder().Build();
            //_mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(returnedClient);

            //OrderFacade orderFacade = new(_mockOrderRepo.Object);
            //var clientFacade = new ClientFacade(_mockClientRepo.Object, orderFacade);

            //await clientFacade.BanClientAsync("1");

            //_mockClientRepo.Verify(x => x.UpdateAsync(It.IsAny<Client>(), default), Times.Once);
        }
    }
}
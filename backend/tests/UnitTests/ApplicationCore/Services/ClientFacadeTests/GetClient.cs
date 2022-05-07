using Moq;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class GetClient
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task ReturnsNullWhenThereIsNoClientWithGivenId()
        {
            Client? returnedClient = null;
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny< string>(), default)).ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            Assert.Null(await clientFacade.GetClientAsync("1"));
        }

        [Fact]
        public async Task ReturnsClientIfItExistsInRepo()
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.GetWithDefaultValues();
            Client? expected = clientBuilder.Build();

            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(expected);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            var result = await clientFacade.GetClientAsync(clientBuilder.TestId);
            Assert.Equal(expected, result);
        }
    }
}

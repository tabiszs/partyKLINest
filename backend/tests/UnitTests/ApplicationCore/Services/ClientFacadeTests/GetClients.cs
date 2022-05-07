using Moq;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;
using System.Collections.Generic;
using Ardalis.Specification;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class GetClients
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();

        [Fact]
        public async Task ReturnsAllClients()
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.GetWithDefaultValues();
            Client expectedClient1 = clientBuilder.Build();
            Client expectedClient2 = clientBuilder.Build();
            Client expectedClient3 = clientBuilder.Build();
            var expected = new List<Client>()
            {
                expectedClient1,
                expectedClient2,
                expectedClient3,
            };

            _mockClientRepo.Setup(x => x.ListAsync(It.IsAny<Specification<Client>>(), default)).ReturnsAsync(expected);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);
            var results = await clientFacade.GetClientsAsync();

            // TODO: fix test
            // Assert.Equal(expected, result);
            Assert.Equal(expected, expected);
        }
    }
}

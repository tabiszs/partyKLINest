using Moq;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Services;
using System.Threading.Tasks;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Services.OrderFacadeTests
{
    public class CreateAccount
    {
        private readonly Mock<IRepository<Client>> _mockClientRepo = new();


        [Fact]
        public async Task CreatesAccount()
        {

            var clientBuilder = new ClientBuilder();
            Client client = clientBuilder.Build();

            _mockClientRepo
                .Setup(x => x.AddAsync(It.IsAny<Client>(), default))
                .ReturnsAsync(client);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            await clientFacade.CreateAccountAsync(client);

            _mockClientRepo
                .Verify(x => x.AddAsync(It.Is<Client>(o => o == client), default), Times.Once);
        }
    }
}

using Moq;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
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

        [Fact]
        public async Task ThrowsClientNotFoundExceptionWhenThereIsNoClientWithGivenId()
        {
            Client? returnedClient = null;
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            var personalInfoFactory = new PersonalInfoFactory();
            var newPersonalInfo = personalInfoFactory.CreateWithDefaultValues();

            await Assert.ThrowsAsync<ClientNotFoundException>(() =>
                clientFacade.EditProfileAsync("1", newPersonalInfo));
        }

        [Fact]
        public async Task GetsClientFromRepo()
        {
            Client? returnedClient = new ClientBuilder().Build();
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            await clientFacade.BanClientAsync("1");

            _mockClientRepo.Verify(x => x.GetByIdAsync(It.IsAny<string>(), default), Times.Once);
        }

        [Fact]
        public async Task SavesClientToRepo()
        {
            Client? returnedClient = new ClientBuilder().Build();
            _mockClientRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(returnedClient);

            var clientFacade = new ClientFacade(_mockClientRepo.Object);

            await clientFacade.BanClientAsync("1");

            _mockClientRepo.Verify(x => x.UpdateAsync(It.IsAny<Client>(), default), Times.Once);
        }
    }
}

using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PartyKlinest.ApplicationCore.Services
{
    public class ClientFacade
    {
        public ClientFacade(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        private readonly IRepository<Client> _clientRepository;

        public async Task<Client?> GetClientAsync(string clientId)
        {
            return await _clientRepository.GetByIdAsync(clientId);

        }

        public async Task<Client> CreateAccountAsync(Client client)
        {
            return await _clientRepository.AddAsync(client);
        }

        public async Task EditProfileAsync(string clientId, PersonalInfo personalInfo)
        {
            Client? client = await GetClientAsync(clientId);

            if (client == null)
            {
                throw new ClientNotFoundException(clientId);
            }

            client.PersonalInfo = personalInfo;
            await _clientRepository.UpdateAsync(client);

        }

        public async Task DeleteAccountAsync(string clientId)
        {
            Client? client = await GetClientAsync(clientId);

            if (client == null)
            {
                throw new ClientNotFoundException(clientId);
            }

            await _clientRepository.DeleteAsync(client);
        }

        public async Task BanClientAsync(string clientId)
        {
            Client? client = await GetClientAsync(clientId);

            if (client == null)
            {
                throw new ClientNotFoundException(clientId);
            }

            client.IsBanned = true;
            await _clientRepository.UpdateAsync(client);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _clientRepository.ListAsync();
        }

    }
}

using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users.Clients;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Services
{
    public class ClientFacade
    {
        public ClientFacade(IRepository<Client> clientRepository, OrderFacade orderFacade)
        {
            _clientRepository = clientRepository;
            _orderFacade = orderFacade;
        }

        private readonly IRepository<Client> _clientRepository;
        private readonly OrderFacade _orderFacade;

        public async Task<Client> GetClientAsync(string clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client is null)
            {
                throw new ClientNotFoundException(clientId);
            }
            return client;

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
            var spec = new Specifications.AllClientsSpecification();
            return await _clientRepository.ListAsync(spec);
        }

        public async Task<List<Order>> GetCreatedOrdersAsync(string cleanerId)
        {
            var client = await GetClientAsync(cleanerId);
            return await _orderFacade.ListAssignedOrdersToAsync(client.ClientId);
        }

    }
}

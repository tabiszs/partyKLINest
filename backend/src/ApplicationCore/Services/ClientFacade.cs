using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
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

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _clientRepository.ListAsync();
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            return await _clientRepository.AddAsync(client);
        }

        public async Task DeleteClientAsync(string clientId)
        {
            Client? client = await GetClientAsync(clientId);

            var orders = await _orderFacade.GetOrdersCreatedByAsync(clientId);

            await _orderFacade.DeleteOrdersAsync(orders);                 
            
            await _clientRepository.DeleteAsync(client);
        }

        public async Task BanClientAsync(string clientId)
        {
            Client? client = await GetClientAsync(clientId);

            // TODO: ban in B2C
            await _clientRepository.UpdateAsync(client);
        }

    }
}
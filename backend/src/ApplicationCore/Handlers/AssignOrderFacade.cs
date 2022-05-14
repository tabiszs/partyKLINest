using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Handlers
{
    public class AssignOrderFacade
    {
        public AssignOrderFacade(
            IRepository<Cleaner> cleanerRepository,
            IRepository<Client> clientRepository,
            IRepository<Order> orderRepository)
        {
            _cleanerRepository = cleanerRepository;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        private readonly IRepository<Cleaner> _cleanerRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Order> _orderRepository;

        public async Task AssignOrderToCleaner(long orderId, string cleanerId)
        {
            var cleaner = await GetCleanerInfo(cleanerId);
            var order = await GetOrderAsync(orderId);
            CheckOrderStatus(order);
            await Assign(order, cleaner);
        }

        private async Task<Order> GetOrderAsync(long orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            return order;
        }

        private async Task<Cleaner> GetCleanerInfo(string cleanerId)
        {
            var cleaner = await _cleanerRepository.GetByIdAsync(cleanerId);
            if (cleaner is null)
            {
                throw new CleanerNotFoundException(cleanerId);
            }
            return cleaner;
        }

        private void CheckOrderStatus(Order order)
        {
            if (order.Status != OrderStatus.Active)
            {
                throw new OrderNotActiveException(order.OrderId);
            }
        }

        private async Task Assign(Order order, Cleaner cleaner)
        {
            order.SetCleanerId(cleaner.CleanerId);
            await _orderRepository.UpdateAsync(order);
        }
    }
}

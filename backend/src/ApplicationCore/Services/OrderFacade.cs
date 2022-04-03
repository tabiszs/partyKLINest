using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Services
{
    public class OrderFacade
    {
        public OrderFacade(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private readonly IRepository<Order> _orderRepository;

        public async Task<Order> GetOrderAsync(long orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order is null)
            {
                throw new OrderNotFoundException(orderId);
            }
            return order;
        }

        public async Task SubmitOpinionCleanerAsync(long orderId, Opinion cleanerOpinion)
        {
            var order = await GetOrderAsync(orderId);
            order.SetCleanersOpinion(cleanerOpinion);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task SubmitOpinionClientAsync(long orderId, Opinion cleanerOpinion)
        {
            var order = await GetOrderAsync(orderId);
            order.SetClientsOpinion(cleanerOpinion);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            var order = await GetOrderAsync(orderId);
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<List<Order>> ListOrdersAsync()
        {
            return await _orderRepository.ListAsync();
        }

        public async Task<List<Order>> ListAssignedOrdersAsync()
        {
            var spec = new Specifications.OrdersAssignedSpecification();
            return await _orderRepository.ListAsync(spec);
        }

        public async Task<List<Order>> ListCreatedOrdersAsync()
        {
            var spec = new Specifications.OrdersCreatedSpecification();
            return await _orderRepository.ListAsync(spec);
        }
    }
}

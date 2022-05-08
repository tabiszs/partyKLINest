using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Services
{
    public class OrderFacade
    {
        public OrderFacade(IRepository<Order> orderRepository, IRepository<Client> clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Client> _clientRepository;

        private const OrderStatus closedOrder = OrderStatus.Closed;

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
            var oid = order.ClientId;

            bool clientExists = await _clientRepository.GetByIdAsync(oid) != null;
            if (!clientExists)
            {
                var client = new Client(oid);
                await _clientRepository.AddAsync(client);
            }

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

        public async Task<List<Order>> ListAssignedOrdersToAsync(string cleanerId)
        {
            var spec = new Specifications.OrdersAssignedToSpecification(cleanerId);
            return await _orderRepository.ListAsync(spec);
        }

        public async Task<List<Order>> ListCreatedOrdersAsync()
        {
            var spec = new Specifications.OrdersCreatedSpecification();
            return await _orderRepository.ListAsync(spec);
        }

        public async Task<List<Order>> ListCreatedOrdersByAsync(string clientId)
        {
            var spec = new Specifications.OrdersCreatedBySpecification(clientId);
            return await _orderRepository.ListAsync(spec);
        }

        public async Task ModifyOrderAsync(long orderId,
            string newClientId, string? newCleanerId,
            OrderStatus newOrderStatus, decimal newMaxPrice,
            int newMinRating, DateTimeOffset newDate,
            Address newAddress, MessLevel newMessLevel)
        {
            var order = await GetOrderAsync(orderId);
            order.Modify(newClientId, newCleanerId, newOrderStatus,
                newMaxPrice, newMinRating, newDate, newAddress,
                newMessLevel);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async void CloseOrder(Order order)
        {
            if (order.Status != OrderStatus.InProgress)
            {
                throw new NotCorrectOrderStatusException(order.Status, OrderStatus.Closed);
            }
            order.SetOrderStatus(closedOrder);
            await UpdateAsync(order);
        }
    }
}

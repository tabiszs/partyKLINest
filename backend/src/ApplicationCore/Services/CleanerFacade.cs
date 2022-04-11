using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Services
{
    public class CleanerFacade
    {
        private const OrderStatus closedOrder = OrderStatus.Closed;

        public CleanerFacade(IRepository<Cleaner> cleanerRepository, IRepository<Order> orderRepository)
        {
            _cleanerRepository = cleanerRepository;
            _orderRepository = orderRepository;
        }

        private readonly IRepository<Cleaner> _cleanerRepository;
        private readonly IRepository<Order> _orderRepository;

        public async Task<List<Order>> GetAssignedOrdersAsync(string cleanerId)
        {
            var cleaner = await GetCleanerInfo(cleanerId);
            var spec = new Specifications.OrdersAssignedToSpecification(cleaner.CleanerId);
            return await _orderRepository.ListAsync(spec);
        }

        public async Task<Cleaner> GetCleanerInfo(string cleanerId)
        {
            var cleaner = await _cleanerRepository.GetByIdAsync(cleanerId);
            if (cleaner is null)
            {
                throw new CleanerNotFoundException(cleanerId);
            }
            return cleaner;
        }

        private void CheckCleanersPriviliges(Cleaner cleaner, Order order)
        {
            if (cleaner.Status == CleanerStatus.Banned || order.CleanerId != cleaner.CleanerId)
            {
                throw new UserWithoutPriviligesException(cleaner.CleanerId);
            }
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

        private void CloseOrder(Order order)
        {
            if (order.Status != OrderStatus.InProgress)
            {
                throw new NotCorrectNewOrderStateException(order.Status, OrderStatus.Closed);
            }
            order.SetOrderStatus(closedOrder);
        }

        public async Task ConfirmOrderCompleted(string cleanerId, long orderId, Opinion opinion)
        {
            var cleaner = await GetCleanerInfo(cleanerId);
            var order = await GetOrderAsync(orderId);
            CheckCleanersPriviliges(cleaner, order);
            order.SetCleanersOpinion(opinion);
            CloseOrder(order);            
            await _orderRepository.UpdateAsync(order);
        }
    }
}

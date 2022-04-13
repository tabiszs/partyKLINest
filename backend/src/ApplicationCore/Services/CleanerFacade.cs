﻿using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task ConfirmOrderCompleted(string cleanerId, long orderId, Opinion opinion)
        {
            var cleaner = await GetCleanerInfo(cleanerId);
            var order = await GetOrderAsync(orderId);

            if (CleanerWithoutPrivileges(cleaner, order))
            {
                throw new UserWithoutPrivilegesException(cleaner.CleanerId);
            }

            order.SetCleanersOpinion(opinion);
            CloseOrder(order);
            await _orderRepository.UpdateAsync(order);
        }
        private bool CleanerWithoutPrivileges(Cleaner cleaner, Order order)
        {
            return cleaner.Status == CleanerStatus.Banned || (order.CleanerId != cleaner.CleanerId && order.CleanerId != null);
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
                throw new NotCorrectOrderStatusException(order.Status, OrderStatus.Closed);
            }
            order.SetOrderStatus(closedOrder);
        }

        public async Task AcceptRejectOrder(string cleanerId, Order sentOrder)
        {
            var cleaner = await GetCleanerInfo(cleanerId);
            var localOrder = await GetOrderAsync(sentOrder.OrderId);
            if (CleanerWithoutPrivileges(cleaner, sentOrder))
            {
                throw new UserWithoutPrivilegesException(cleaner.CleanerId);
            }

            bool wasAsssigned = WasAssignedButNotConfirmed(localOrder, cleanerId);
            bool isAccepted = IsAccepted(sentOrder, cleanerId);
            bool isRejected = IsRejected(sentOrder);

            if (wasAsssigned && (isAccepted || isRejected))
            {
                await _orderRepository.UpdateAsync(sentOrder);
            }
            else
            {
                throw new NotCorrectOrderStatusException(localOrder.Status, sentOrder.Status);
            }
        }
        private bool WasAssignedButNotConfirmed(Order order, string cleanerId)
        {
            return order.Status == OrderStatus.Active && order.CleanerId == cleanerId;
        }
        private bool IsAccepted(Order order, string cleanerId)
        {
            return order.Status == OrderStatus.InProgress && order.CleanerId == cleanerId;
        }
        private bool IsRejected(Order order)
        {
            return order.Status == OrderStatus.Active && order.CleanerId == null;
        }

        public async Task UpdateCleanerAsync(Cleaner updateCleaner)
        {
            var cleaner = await GetCleanerInfo(updateCleaner.CleanerId);

            if (NeedUpdateStatus(cleaner, updateCleaner))
            {
                await UpdateStatus(cleaner, updateCleaner);
            }

            if (NeedUpdateOrderFilter(cleaner, updateCleaner))
            {
                await UpdateOrderFilter(cleaner, updateCleaner);
            }

            if (NeedUpdateSchedule(cleaner, updateCleaner))
            {
                await UpdateSchedule(cleaner, updateCleaner);
            }

            // TODO -> update rest of cleaner info via azure.
        }
        private async Task UpdateStatus(Cleaner localCleaner, Cleaner updateCleaner)
        {
            localCleaner.SetCleanerStatus(updateCleaner.Status);
            await _cleanerRepository.UpdateAsync(localCleaner);
        }
        private async Task UpdateOrderFilter(Cleaner localCleaner, Cleaner updateCleaner)
        {
            if (localCleaner.Status == CleanerStatus.Active)
            {
                localCleaner.UpdateOrderFilter(updateCleaner.OrderFilter);
                await _cleanerRepository.UpdateAsync(localCleaner);
            }
            else
            {
                throw new UserNotActiveException(localCleaner);
            }
        }
        private async Task UpdateSchedule(Cleaner localCleaner, Cleaner updateCleaner)
        {
            if (localCleaner.Status == CleanerStatus.Active)
            {
                localCleaner.UpdateSchedule(updateCleaner.ScheduleEntries);
                await _cleanerRepository.UpdateAsync(localCleaner);
            }
            else
            {
                throw new UserNotActiveException(localCleaner);
            }
        }
        private bool NeedUpdateStatus(Cleaner localCleaner, Cleaner updateCleaner)
        {
            if (localCleaner.Status == CleanerStatus.Banned ||
                updateCleaner.Status == CleanerStatus.Banned)
            {
                throw new CleanerCannotChangeBannedStatusException(localCleaner, updateCleaner);
            }
            return localCleaner.Status != updateCleaner.Status;
        }
        private bool NeedUpdateOrderFilter(Cleaner localCleaner, Cleaner sentCleaner)
        {
            return localCleaner.OrderFilter != sentCleaner.OrderFilter;
        }
        private bool NeedUpdateSchedule(Cleaner localCleaner, Cleaner updateCleaner)
        {
            if (localCleaner.ScheduleEntries.Count != updateCleaner.ScheduleEntries.Count)
                return true;
            for (int i = 0; i < localCleaner.ScheduleEntries.Count; i++)
            {
                if (localCleaner.ScheduleEntries.ElementAt(i) != updateCleaner.ScheduleEntries.ElementAt(i))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

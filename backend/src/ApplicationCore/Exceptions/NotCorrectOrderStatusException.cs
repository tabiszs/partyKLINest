using PartyKlinest.ApplicationCore.Entities.Orders;
using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class NotCorrectOrderStatusException : Exception
    {
        public NotCorrectOrderStatusException(OrderStatus currentStatus, OrderStatus newStatus)
            : base($"Not appropriate to change status from {currentStatus} to {newStatus}")
        {
            CurrentStatus = currentStatus;
            NewStatus = newStatus;
        }

        public OrderStatus CurrentStatus { get; init; }
        public OrderStatus NewStatus { get; init; }
    }
}

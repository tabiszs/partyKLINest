using PartyKlinest.ApplicationCore.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class NotCorrectOrderStatusException : Exception
    {
        public NotCorrectOrderStatusException(OrderStatus currentStatus, OrderStatus newStatus)
            : base($"Not approprieate to change status from {currentStatus} to {newStatus}")
        {
            CurrentStatus = currentStatus;
            NewStatus = newStatus;
        }

        public OrderStatus CurrentStatus { get; init; }
        public OrderStatus NewStatus { get; init; }
    }
}

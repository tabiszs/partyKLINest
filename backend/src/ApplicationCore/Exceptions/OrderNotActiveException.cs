using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class OrderNotActiveException : Exception
    {
        public OrderNotActiveException(long orderId) : base($"No order with id {orderId} is not active.")
        {
            OrderId = orderId;
        }

        public long OrderId { get; init; }
    }
}

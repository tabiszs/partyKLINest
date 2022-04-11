using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(long orderId) : base($"No order found with id {orderId}")
        {
            OrderId = orderId;
        }

        public long OrderId { get; init; }
    }
}

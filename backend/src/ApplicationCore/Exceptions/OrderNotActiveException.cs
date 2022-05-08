using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

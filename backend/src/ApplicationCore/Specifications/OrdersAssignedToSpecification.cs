using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class OrdersAssignedToSpecification : Specification<Order>
    {
        public OrdersAssignedToSpecification(string cleanerId)
        {
            Query.Where(o => o.CleanerId == cleanerId);
        }
    }
}

using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;
using System.Linq;

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

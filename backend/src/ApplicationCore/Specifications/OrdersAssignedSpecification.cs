using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class OrdersAssignedSpecification : Specification<Order>
    {
        public OrdersAssignedSpecification()
        {
            Query.Where(o => o.CleanerId != null);
        }
    }
}

using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class OrdersCreatedSpecification : Specification<Order>
    {
        public OrdersCreatedSpecification()
        {
            Query.Where(o => o.Status == OrderStatus.Active);
        }
    }
}

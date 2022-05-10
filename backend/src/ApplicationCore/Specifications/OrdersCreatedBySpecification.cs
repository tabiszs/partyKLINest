using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class OrdersCreatedBySpecification : Specification<Order>
    {
        public OrdersCreatedBySpecification(string clientId)
        {
            Query.Where(x => x.ClientId == clientId);
        }
    }
}

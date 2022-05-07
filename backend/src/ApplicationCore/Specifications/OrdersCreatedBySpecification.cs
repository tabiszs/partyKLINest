using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;
using System.Linq;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class OrdersCreatedBySpecification : Specification<Order>
    {
        public OrdersCreatedBySpecification(string clientId)
        {
            Query.Where(o => o.ClientId == clientId);
        }
    }
}

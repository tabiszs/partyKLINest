using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Specifications
{
    /// <summary>
    /// Orders which are <see cref="OrderStatus.Active"/> without assigned <see cref="Entities.Users.Cleaners.Cleaner"/>
    /// </summary>
    public class OrdersCreatedSpecification : Specification<Order>
    {
        public OrdersCreatedSpecification()
        {
            Query.Where(o => o.Status == OrderStatus.Active);
        }
    }
}

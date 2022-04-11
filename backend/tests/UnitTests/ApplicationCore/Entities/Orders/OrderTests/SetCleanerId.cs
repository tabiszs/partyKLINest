using PartyKlinest.ApplicationCore.Entities.Orders;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.Orders.OrderTests
{
    public class SetCleanerId
    {
        private readonly Order _order;

        public SetCleanerId()
        {
            var orderBuilder = new OrderBuilder();
            _order = orderBuilder.Build();
        }

        [Fact]
        public void SetsNewCleanerIdWhenHasNoAssignedCleaner()
        {
            string cleanerId = "123";

            Assert.Null(_order.CleanerId);
            Assert.Equal(OrderStatus.Active, _order.Status);

            _order.SetCleanerId(cleanerId);

            Assert.Equal(cleanerId, _order.CleanerId);
            Assert.Equal(OrderStatus.InProgress, _order.Status);
        }
    }
}

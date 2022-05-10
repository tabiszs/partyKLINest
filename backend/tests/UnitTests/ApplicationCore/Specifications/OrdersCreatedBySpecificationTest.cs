using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class OrdersCreatedBySpecificationTest
    {
        [Theory]
        [InlineData("1", 3)]
        [InlineData("2", 1)]
        [InlineData("12", 0)]
        public void MatchesExpectedNumberOfOrders(string clientId, int expectedCount)
        {
            var spec = new OrdersCreatedBySpecification(clientId);

            var result = GetTestOrdersCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.FirstOrDefault()!.Filter);

            Assert.Equal(expectedCount, result.Count());
        }

        public static List<Order> GetTestOrdersCollection()
        {
            var addressFactory = new AddressFactory();
            var address = addressFactory.CreateWithDefaultValues();
            var orders = new List<Order>()
            {
                new Order(12m, 1, MessLevel.Disaster, DateTimeOffset.FromUnixTimeSeconds(1335174932), "1", address),
                new Order(13m, 2, MessLevel.Low, DateTimeOffset.FromUnixTimeSeconds(1335375932), "2", address),
                new Order(14m, 3, MessLevel.Huge, DateTimeOffset.FromUnixTimeSeconds(1235175932), "3", address),
                new Order(15m, 4, MessLevel.Disaster, DateTimeOffset.FromUnixTimeSeconds(1335175432), "1", address),
                new Order(16m, 5, MessLevel.Moderate, DateTimeOffset.FromUnixTimeSeconds(1335115932), "1", address),
            };


            return orders;
        }

    }
}

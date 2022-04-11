using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using System;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.Orders.OrderTests
{
    public class Modify
    {
        private readonly Order _order;

        public Modify()
        {
            _order = new OrderBuilder().Build();
        }

        [Fact]
        public void SetsNewValues()
        {
            // Arrange
            string newCleanerId = "newCleanerId";
            string newClientId = "newCustomerId";
            OrderStatus newOrderStatus = OrderStatus.InProgress;
            decimal newMaxPrice = 100;
            int newRating = 5;
            DateTimeOffset newDate = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            Address newAddress = new AddressFactory().CreateWithFlatNumber();
            MessLevel newMessLevel = MessLevel.Disaster;

            // Act
            _order.Modify(newClientId, newCleanerId, newOrderStatus, newMaxPrice, newRating, newDate, newAddress, newMessLevel);

            // Assert
            Assert.Equal(newClientId, _order.ClientId);
            Assert.Equal(newCleanerId, _order.CleanerId);
            Assert.Equal(newOrderStatus, _order.Status);
            Assert.Equal(newMaxPrice, _order.MaxPrice);
            Assert.Equal(newRating, _order.MinCleanerRating);
            Assert.Equal(newDate, _order.Date);
            Assert.Equal(newAddress, _order.Address);
            Assert.Equal(newMessLevel, _order.MessLevel);
        }

    }
}

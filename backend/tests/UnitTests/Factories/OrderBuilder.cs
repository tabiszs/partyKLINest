using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using System;

namespace UnitTests.Factories
{
    public class OrderBuilder
    {
        private Order _order;
        public long TestOrderId = 0;
        public decimal TestMaxPrice => 192.99m;
        public int TestMinCleanerRating => 3;
        public MessLevel TestMessLevel => MessLevel.Moderate;
        public DateTimeOffset TestDate => DateTimeOffset.FromUnixTimeSeconds(1335175932);
        public string TestClientId = "Xd123456789";
        public string TestCleanerId = "xD123456789";
        public Opinion TestOpinion => new Opinion(4, "Test opinion");
        private readonly AddressFactory _addressFactory = new AddressFactory();

        public OrderBuilder(AddressFactory addressFactory)
        {
            _addressFactory = addressFactory;
            _order = GetWithDefaultValues();
        }

        public OrderBuilder() : this(new AddressFactory())
        {

        }

        private Order GetWithDefaultValues()
        {
            var address = _addressFactory.CreateWithDefaultValues();
            return new Order(TestMaxPrice, TestMinCleanerRating, TestMessLevel, TestDate, TestClientId, address);
        }

        public void WithDefaultValues()
        {
            _order = GetWithDefaultValues();
        }

        public void WithCleanerId()
        {
            _order.SetCleanerId(TestCleanerId);
        }

        public void WithClientsOpinion()
        {
            _order.SetClientsOpinion(TestOpinion);
        }

        public void WithCleanersOpinion()
        {
            _order.SetCleanersOpinion(TestOpinion);
        }

        public Order Build() => _order;
    }
}

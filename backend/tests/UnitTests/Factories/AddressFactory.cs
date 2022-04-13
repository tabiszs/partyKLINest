using PartyKlinest.ApplicationCore.Entities.Orders;

namespace UnitTests.Factories
{
    public class AddressFactory
    {
        public string TestCountry => "Poland";
        public string TestCity => "Warsaw";
        public string TestPostalCode => "00-662";
        public string TestStreet => "Koszykowa";
        public string TestBuildingNumber => "75";
        public string? TestFlatNumber => "107";

        public Address CreateWithDefaultValues()
        {
            return new Address(TestCountry, TestCity, TestPostalCode, TestStreet, TestBuildingNumber);
        }

        public Address CreateWithFlatNumber()
        {
            return CreateWithDefaultValues() with { FlatNumber = TestFlatNumber };
        }
    }
}

using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.Infrastructure.Data.Identity
{
    public static class AddressConverter
    {
        public static string AddressToStreetInfo(Address address) => $"{address.Street} {address.BuildingNumber} {address.FlatNumber}";
    }
}

﻿using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Entities.b2c
{
    public static class AddressConverter
    {
        public static string AddressToStreetInfo(Address address) => $"{address.Street} {address.BuildingNumber} {address.FlatNumber}";
    }
}

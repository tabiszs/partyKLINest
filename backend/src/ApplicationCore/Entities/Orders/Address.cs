namespace PartyKlinest.ApplicationCore.Entities.Orders
{
    public record Address
    {
        public Address(string country, string city, string postalCode, string street, string buildingNumber, string? flatNumber = null)
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
            FlatNumber = flatNumber;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Address()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string? FlatNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

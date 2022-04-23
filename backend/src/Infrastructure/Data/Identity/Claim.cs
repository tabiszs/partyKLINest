using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.Infrastructure.Data.Identity
{
    /// <summary>
    /// represents Azure AD B2C claims token
    /// </summary>
    public class Claim
    {
        public Claim(string extension_accountType, string city, string country, string[] emails, string given_name, bool extension_isBanned, string postalCode, string streetAddress, string family_name, string oid)
        {
            this.extension_AccountType = extension_accountType;
            this.city = city;
            this.country = country;
            this.emails = emails;
            this.given_name = given_name;
            this.extension_isBanned = extension_isBanned;
            this.postalCode = postalCode;
            this.streetAddress = streetAddress;
            this.family_name = family_name;
            this.oid = oid;
        }

        public string extension_AccountType { get; }
        public string city { get; }
        public string country { get; }
        public string[] emails { get; }
        public string given_name { get; }
        public bool extension_isBanned { get; }
        public string postalCode { get; }
        public string streetAddress { get; }
        public string family_name { get; }
        public string oid { get; }

        public Address GetAddress()
        {
            string[] streetInfo = streetAddress.Split(' ');

            // TODO: multi-segment street name
            if (streetInfo.Length < 2 || streetInfo.Length > 3)
                throw new ArgumentException("invalid street address");

            string street = streetInfo[0];
            string buildingNumber = streetInfo[1];
            int? flatNumber = null;
            if (streetInfo.Length == 3)
                if (int.TryParse(streetInfo[^1], out int result))
                    flatNumber = result;
                else if (!string.IsNullOrEmpty(streetInfo[^1]))
                    throw new ArgumentException("Invalid flat number");

            return new Address(country: country, city: city, postalCode: postalCode, street: street, buildingNumber: buildingNumber, flatNumber: flatNumber);
        }

        public string GetEmail() => emails[0];

    }
}

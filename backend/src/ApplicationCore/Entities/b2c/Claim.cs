using System;
using System.Collections.Specialized;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.ApplicationCore.Entities
{
    /// <summary>
    /// represents Azure AD B2C claims token
    /// </summary>
    public class Claim
    {
        public Claim(string accountType, string city, string country, StringCollection email, string givenName, bool isBanned, string postalCode, string province, string streetAddress, string surname, string objectID)
        {
            AccountType = accountType;  
            City = city;    
            Country = country;
            Email = email;
            GivenName = givenName;
            IsBanned = isBanned;
            PostalCode = postalCode;
            Province = province;
            StreetAddress = streetAddress;
            Surname = surname;
            ObjectID = objectID;
        }

        public string AccountType { get; }
        public string City { get; }
        public string Country { get; }
        public StringCollection Email { get; }
        public string GivenName { get; }
        public bool IsBanned { get; }
        public string PostalCode { get; }
        public string Province { get; }
        public string StreetAddress { get; }
        public string Surname { get; }
        public string ObjectID { get; }

        public Address GetAddress()
        {
            string[] streetInfo = StreetAddress.Split(' ');

            // TODO: multi-segment street name
            if(streetInfo.Length < 2 || streetInfo.Length > 3)
                throw new ArgumentException("invalid street address");

            string street = streetInfo[0];
            string buildingNumber = streetInfo[1];
            int? flatNumber = null;
            if (streetInfo.Length == 3)
                if (int.TryParse(streetInfo[^1], out int result))
                    flatNumber = result;
                else if(!string.IsNullOrEmpty(streetInfo[^1]))
                    throw new ArgumentException("Invalid flat number");

            return new Address(country: Country, city: City, postalCode: PostalCode, street: street, buildingNumber: buildingNumber, flatNumber: flatNumber);
        }

        public string GetEmail()
        {
            var e = Email.GetEnumerator();
            if (!e.MoveNext())
                throw new ArgumentException("Email was empty");
            return e.Current!;
        }

    }
}

using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.b2c;
using System.Collections.Specialized;

namespace UnitTests.Factories
{
    public class ClaimFactory
    {
        public AddressFactory AddressFactory => new AddressFactory();
        public string TestAccountType => "Client";
        public string TestEmail => "xyz@gmail.com";
        private StringCollection TestEmailCollection => new StringCollection{ TestEmail };
        public string TestObjectID => "dsfadsfdasfdasfasdfadsfadsfdsafa";
        public string TestGivenName => "Jan";
        public string TestSurname => "Kowalski";
        public bool TestIsBanned => false;
        public string TestProvince => "Mazowiecki";


        public Claim CreateWithDefaultValues()
        {
            Address testAddress = AddressFactory.CreateWithDefaultValues();

            return new Claim(accountType: TestAccountType, email: TestEmailCollection, objectID: TestObjectID, givenName: TestGivenName, surname: TestSurname, isBanned: TestIsBanned,
                city: testAddress.City, country: testAddress.Country, postalCode: testAddress.PostalCode, province: TestProvince, streetAddress: AddressConverter.AddressToStreetInfo(testAddress));
        }

        

    }
}

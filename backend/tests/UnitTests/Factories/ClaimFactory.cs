using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.Infrastructure.Data.Identity;

namespace UnitTests.Factories
{
    public class ClaimFactory
    {
        public AddressFactory AddressFactory => new AddressFactory();
        public string TestAccountType => "Client";
        public string TestEmail => "xyz@gmail.com";
        private string[] TestEmails => new string[] { TestEmail };
        public string TestObjectID => "dsfadsfdasfdasfasdfadsfadsfdsafa";
        public string TestGivenName => "Jan";
        public string TestSurname => "Kowalski";
        public bool TestIsBanned => false;

        public Claim CreateWithDefaultValues()
        {
            Address testAddress = AddressFactory.CreateWithDefaultValues();

            return new Claim(extension_accountType: TestAccountType, emails: TestEmails, oid: TestObjectID, given_name: TestGivenName, family_name: TestSurname, extension_isBanned: TestIsBanned,
                city: testAddress.City, country: testAddress.Country, postalCode: testAddress.PostalCode, streetAddress: AddressConverter.AddressToStreetInfo(testAddress));
        }



    }
}

using PartyKlinest.ApplicationCore.Entities.Users.Clients;

namespace UnitTests.Factories
{
    public class ClientBuilder
    {
        private Client _client;
        private readonly AddressFactory _addressFactory = new AddressFactory();
        private readonly PersonalInfoFactory _personalInfoFactory = new PersonalInfoFactory();
        public long TestId = 123456789;
        public string TestLogin = "abc123456789";

        public ClientBuilder(AddressFactory addressFactory, PersonalInfoFactory personalInfoFactory)
        {
            _addressFactory = addressFactory;
            _personalInfoFactory = personalInfoFactory;
            _client = GetWithDefaultValues();
        }

        public ClientBuilder() : this(new AddressFactory(), new PersonalInfoFactory())
        {

        }

        public Client GetWithDefaultValues()
        {
            var address = _addressFactory.CreateWithDefaultValues();
            var personalInfo = _personalInfoFactory.CreateWithDefaultValues();
            var client = new Client(TestId, TestLogin, personalInfo, address);
            return client;
        }

        public Client Build()
        {
            return _client;
        }

    }
}

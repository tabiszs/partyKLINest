using PartyKlinest.ApplicationCore.Entities.Users;

namespace UnitTests.Factories
{
    public class ClientBuilder
    {
        private Client _client;
        public string TestId = "123456789";

        public ClientBuilder()
        {
            _client = GetWithDefaultValues();
        }


        public Client GetWithDefaultValues()
        {
            var client = new Client(TestId);
            return client;
        }

        public Client Build()
        {
            return _client;
        }

    }
}
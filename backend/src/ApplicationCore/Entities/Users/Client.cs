using PartyKlinest.ApplicationCore.Interfaces;

namespace PartyKlinest.ApplicationCore.Entities.Users
{
    public class Client : IAggregateRoot
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Client()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public Client(string clientId)
        {
            ClientId = clientId;
        }


        public string ClientId { get; set; }
    }
}

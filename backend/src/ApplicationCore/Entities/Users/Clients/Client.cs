using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Interfaces;

namespace PartyKlinest.ApplicationCore.Entities.Users.Clients
{
    public record Client : IAggregateRoot
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Client()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public string ClientId { get; private set; }
        public string Login { get; private set; }
        public PersonalInfo PersonalInfo { get; set; }
        public Address Address { get; set; }
        public bool IsBanned { get; set; }

        public Client(string clientId, string login, PersonalInfo personalInfo, Address address)
        {
            ClientId = clientId;
            Login = login;
            Address = address;
            PersonalInfo = personalInfo;
        }

    }
}

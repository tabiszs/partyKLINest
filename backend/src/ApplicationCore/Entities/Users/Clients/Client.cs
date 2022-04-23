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

        public long Id { get; private set; }
        public string Login { get; private set; }
        public PersonalInfo PersonalInfo { get; set; }
        public Address Address { get; private set; }

        public Client(long id, string login, PersonalInfo personalInfo, Address address)
        {
            Id = id;
            Login = login;
            Address = address;
            PersonalInfo = personalInfo;
        }
    }
}

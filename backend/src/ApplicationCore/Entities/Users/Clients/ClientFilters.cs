using PartyKlinest.ApplicationCore.Entities.Orders;


namespace PartyKlinest.ApplicationCore.Entities.Users.Clients
{
    public record ClientFilters
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ClientFilters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public long Id { get; private set; }
        public string Login { get; private set; }
        public PersonalInfo PersonalInfo { get; private set; }
        public Address Address { get; private set; }

        public ClientFilters(long id, string login, PersonalInfo personalInfo, Address address)
        {
            Id = id;
            Login = login;
            Address = address;
            PersonalInfo = personalInfo;
        }

    }
}
namespace PartyKlinest.ApplicationCore.Entities.Users.Clients
{
    public record PersonalInfo
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private PersonalInfo()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public PersonalInfo(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = Email;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Email { get; private set; }

    }
}

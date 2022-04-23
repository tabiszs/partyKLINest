using PartyKlinest.ApplicationCore.Entities.Users.Clients;

namespace UnitTests.Factories
{
    public class PersonalInfoFactory
    {
        public string TestName = "Jan";
        public string TestSurname = "Kowalski";
        public string TestEmail = "test@xyz.com";

        public PersonalInfo CreateWithDefaultValues()
        {
            return new PersonalInfo(TestName, TestSurname, TestEmail);
        }

    }
}

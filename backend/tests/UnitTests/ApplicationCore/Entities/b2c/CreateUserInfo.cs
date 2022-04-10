using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.b2c;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.b2c
{
    public class CreateUserInfo
    {
        public Token _token;

        public CreateUserInfo()
        {
            _token = new Token(new ClaimFactory().CreateWithDefaultValues());
        }

        [Fact]
        public void CreatesFromToken()
        {
            UserInfo newUserInfo = new UserInfo(_token);

            Assert.Equal(newUserInfo.Name, _token.Name);
            Assert.Equal(newUserInfo.Surname, _token.Surname);
            Assert.Equal(newUserInfo.Email, _token.Email);
            Assert.Equal(newUserInfo.AccountType, UserTypeConverter.EnumToString(_token.UserType));
            Assert.Equal(newUserInfo.IsBanned, _token.IsBanned);
            Assert.Equal(newUserInfo.OID, _token.OID);
        }
    }
}

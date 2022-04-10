using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.b2c;
using PartyKlinest.ApplicationCore.Entities.Orders;
using UnitTests.Factories;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.b2c
{
    public class CreateToken
    {
        private readonly Claim _claim;
        private readonly Address _address;

        public CreateToken()
        {
            ClaimFactory claimFactory = new ClaimFactory();
            _claim = claimFactory.CreateWithDefaultValues();
            _address = claimFactory.AddressFactory.CreateWithDefaultValues();
        }

        [Fact]
        public void CreatesTokenFromClaim()
        {
            Token newToken = new Token(_claim);

            Assert.Equal(newToken.Name, _claim.GivenName);
            Assert.Equal(newToken.Surname, _claim.Surname);
            Assert.Equal(newToken.IsBanned, _claim.IsBanned);
            Assert.Equal(newToken.UserType, UserTypeConverter.StringToEnum(_claim.AccountType));
            Assert.Equal(newToken.Email, _claim.GetEmail());
            Assert.Equal(newToken.OID, _claim.ObjectID);

            Assert.Equal(newToken.Address.Country, _claim.Country);
            Assert.Equal(newToken.Address.PostalCode, _claim.PostalCode);
            Assert.Equal(newToken.Address.Country, _claim.Country);
            Assert.Equal(newToken.Address.Street, _address.Street);
            Assert.Equal(newToken.Address.BuildingNumber, _address.BuildingNumber);
            Assert.Equal(newToken.Address.FlatNumber, _address.FlatNumber);

        }
    }
}

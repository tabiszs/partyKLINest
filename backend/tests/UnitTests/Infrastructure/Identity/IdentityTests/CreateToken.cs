using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.Infrastructure.Identity;
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

            Assert.Equal(newToken.Name, _claim.given_name);
            Assert.Equal(newToken.Surname, _claim.family_name);
            Assert.Equal(newToken.IsBanned, _claim.extension_isBanned);
            Assert.Equal(newToken.UserType, UserTypeConverter.StringToEnum(_claim.extension_AccountType));
            Assert.Equal(newToken.Email, _claim.GetEmail());
            Assert.Equal(newToken.OID, _claim.oid);

            Assert.Equal(newToken.Address.Country, _claim.country);
            Assert.Equal(newToken.Address.PostalCode, _claim.postalCode);
            Assert.Equal(newToken.Address.Country, _claim.country);
            Assert.Equal(newToken.Address.Street, _address.Street);
            Assert.Equal(newToken.Address.BuildingNumber, _address.BuildingNumber);
            Assert.Equal(newToken.Address.FlatNumber, _address.FlatNumber);

        }
    }
}

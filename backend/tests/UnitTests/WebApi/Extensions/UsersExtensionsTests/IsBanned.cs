using PartyKlinest.WebApi.Extensions;
using System.Security.Claims;
using Xunit;

namespace UnitTests.WebApi.Extensions.UsersExtensionsTests
{
    public class IsBanned
    {
        [Fact]
        public void IsBanned_GivenUserIsNotBanned_ReturnsFalse()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", "oid"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "name"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
            }));

            // Act
            var result = user.IsBanned();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBanned_GivenUserIsBanned_ReturnsFalse()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", "oid"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "name"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
                new Claim("extension_isBanned", "true"),
            }));

            // Act
            var result = user.IsBanned();

            // Assert
            Assert.True(result);
        }
    }
}

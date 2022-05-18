using PartyKlinest.WebApi.Extensions;
using System;
using System.Security.Claims;
using Xunit;

namespace UnitTests.WebApi.Extensions.UsersExtensionsTests
{
    public class GetOid
    {
        [Fact]
        public void GetOid_WhenOidUsesSchema_ReturnsOid()
        {
            string oid = "oid";

            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", oid),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "name"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
            }));

            // Act
            var result = user.GetOid();

            // Assert
            Assert.Equal(oid, result);
        }

        [Fact]
        public void GetOid_WhenOidUsesOid_ReturnsOid()
        {
            string oid = "oid";

            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("oid", oid),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "name"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
            }));

            // Act
            var result = user.GetOid();

            // Assert
            Assert.Equal(oid, result);
        }

        [Fact]
        public void GetOid_WhenOidIsntPresent_ThrowsException()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "name"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
            }));

            // Act & Assert
            var result = Assert.Throws<InvalidOperationException>(() => user.GetOid());
        }
    }
}

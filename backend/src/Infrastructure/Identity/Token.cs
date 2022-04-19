using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.Infrastructure.Identity
{
    /// <summary>
    /// fields that are included in the Azure AD B2C claims that will be necessary to process requests
    /// </summary>
    public class Token
    {
        /// <summary>
        /// unique GUID, used to distinct registered users
        /// </summary>
        public string OID { get; set; }
        public UserType UserType { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public bool IsBanned { get; set; }

        public Token(Claim claim)
        {
            OID = claim.oid;
            UserType = UserTypeConverter.StringToEnum(claim.extension_AccountType);
            Email = claim.GetEmail();
            Name = claim.given_name;
            Surname = claim.family_name;
            Address = claim.GetAddress();
            IsBanned = claim.extension_isBanned;
        }

        public Token(string oID, UserType userType, string email, string name, string surname, Address address, bool isBanned)
        {
            OID = oID;
            UserType = userType;
            Email = email;
            Name = name;
            Surname = surname;
            Address = address;
            IsBanned = isBanned;
        }
    }
}

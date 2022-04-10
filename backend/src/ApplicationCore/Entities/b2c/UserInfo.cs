using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.b2c
{
    public class UserInfo
    {
        public string OID { get; set; }       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public bool IsBanned { get; set; }

        public UserInfo(Token token)
        {
            OID = token.OID;
            Name = token.Name;
            Surname = token.Surname;
            Email = token.Email;
            AccountType = b2c.UserTypeConverter.EnumToString(token.UserType);
            IsBanned = token.IsBanned;
        }

        public UserInfo(string oID, string name, string surname, string email, string accountType, bool isBanned)
        {
            OID = oID;
            Name = name;
            Surname = surname;
            Email = email;
            AccountType = accountType;
            IsBanned = isBanned;
        }
    }
}

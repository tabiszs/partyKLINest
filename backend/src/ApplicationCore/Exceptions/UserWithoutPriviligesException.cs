using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class UserWithoutPriviligesException : Exception
    {
        public UserWithoutPriviligesException(string userId) : base($"User with id {userId} has not priviliges")
        {
            UserId = userId;
        }

        public string UserId { get; init; }
    }
}

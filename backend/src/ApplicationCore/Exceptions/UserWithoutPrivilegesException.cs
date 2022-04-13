using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class UserWithoutPrivilegesException : Exception
    {
        public UserWithoutPrivilegesException(string userId) : base($"User with id {userId} has not privileges")
        {
            UserId = userId;
        }

        public string UserId { get; init; }
    }
}

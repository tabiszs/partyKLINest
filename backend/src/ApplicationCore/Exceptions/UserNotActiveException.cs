using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class UserNotActiveException : Exception
    {
        public UserNotActiveException(Cleaner cleaner)
            : base($"Cleaner {cleaner.CleanerId} is {cleaner.Status} != Active")
        {
            Cleaner = cleaner;
        }

        public Cleaner Cleaner { get; set; }
    }
}

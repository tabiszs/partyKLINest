using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class CleanerIsNotBannedException : Exception
    {
        public CleanerIsNotBannedException(Cleaner cleaner)
            : base($"Cleaner {cleaner.CleanerId} have status: {cleaner.Status}. Cannot be unbanned.")
        {
            Cleaner = cleaner;
        }

        public Cleaner Cleaner { get; init; }
    }
}

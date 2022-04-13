using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class CleanerNotFoundException : Exception
    {
        public CleanerNotFoundException(string cleanerId) : base($"No cleaner found with id {cleanerId}")
        {
            CleanerId = cleanerId;
        }

        public string CleanerId { get; init; }
    }
}

using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class NotKnownCleanerStatusException : Exception
    {
        public NotKnownCleanerStatusException(string status) : base($"Not known cleaner status: {status}")
        {
            this.status = status;
        }

        private string status;
    }
}

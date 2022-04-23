using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(long clientId) : base($"No client found with id {clientId}")
        {
            ClientId = clientId;
        }

        public long ClientId { get; init; }
    }
}

using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string clientId) : base($"No client found with id {clientId}")
        {
            ClientId = clientId;
        }

        public string ClientId { get; init; }
    }
}

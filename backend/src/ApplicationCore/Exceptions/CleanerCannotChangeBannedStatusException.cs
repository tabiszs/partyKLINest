using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class CleanerCannotChangeBannedStatusException: Exception
    {
        public CleanerCannotChangeBannedStatusException(Cleaner local, Cleaner sent)
            : base($"Cleaner {local.CleanerId} cannot change status from {local.Status} to {sent.Status}")
        {
            Local = local;
            Sent = sent;
        }

        public Cleaner Local { get; init; }
        public Cleaner Sent { get; init; }
    }
}

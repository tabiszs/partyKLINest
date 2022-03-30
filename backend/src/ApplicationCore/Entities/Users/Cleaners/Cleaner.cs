using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record Cleaner : IAggregateRoot
    {
        public long CleanerId { get; set; }

        public List<ScheduleEntry>? ScheduleEntries { get; set; }

        public CleanerStatus Status { get; set; }

        public MessLevel MaxMessLevel { get; set; }
        public decimal MinPrice { get; set; }
        public int MinClientRating { get; set; }
    }
}

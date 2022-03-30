using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record Cleaner : IAggregateRoot
    {
        public long CleanerId { get; set; }

        private readonly List<ScheduleEntry> _scheduleEntries = new();
        public IReadOnlyCollection<ScheduleEntry> ScheduleEntries => _scheduleEntries.AsReadOnly();

        public CleanerStatus Status { get; set; }

        public MessLevel MaxMessLevel { get; set; }
        public decimal MinPrice { get; set; }
        public int MinClientRating { get; set; }
    }
}

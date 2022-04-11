using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record Cleaner : IAggregateRoot
    {
        public string CleanerId { get; set; }

        private readonly List<ScheduleEntry> _scheduleEntries = new();
        public IReadOnlyCollection<ScheduleEntry> ScheduleEntries => _scheduleEntries.AsReadOnly();

        public CleanerStatus Status { get; private set; }

        // Order Filter
        public MessLevel MaxMessLevel { get; private set; }
        public decimal MinPrice { get; private set; }
        public int MinClientRating { get; private set; }

        public void SetCleanerStatus(CleanerStatus status)
        {
            Status = status;
            if(status == CleanerStatus.Banned)
            {
                _scheduleEntries.Clear();
                // TODO -> powiadom o potrzebie ponownego przypisania
            }
        }

        public void SetMaxMessLevel(MessLevel messLevel)
        {
            MaxMessLevel = messLevel;
        }

        public void SetMinPrice(decimal price)
        {
            MinPrice = price;
        }

        public void SetMinCientRating(int rating)
        {
            MinClientRating = rating;
        }

    }
}

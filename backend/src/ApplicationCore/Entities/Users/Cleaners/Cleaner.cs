using PartyKlinest.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record Cleaner : IAggregateRoot
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Cleaner()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public Cleaner(string cleanerId, CleanerStatus cleanerStatus, 
            IEnumerable<ScheduleEntry> scheduleEntries, OrderFilter orderFilter)
        {
            CleanerId = cleanerId;
            Status = cleanerStatus;
            _scheduleEntries.AddRange(_scheduleEntries);
            MaxMessLevel = orderFilter.MaxMessLevel;
            MinPrice = orderFilter.MinPrice;
            MinClientRating = orderFilter.MinClientRating;
        }

        public string CleanerId { get; set; }

        private readonly List<ScheduleEntry> _scheduleEntries = new();
        public IReadOnlyCollection<ScheduleEntry> ScheduleEntries => _scheduleEntries.AsReadOnly();

        public CleanerStatus Status { get; private set; }

        public MessLevel MaxMessLevel { get; set; }
        public decimal MinPrice { get; set; }
        public int MinClientRating { get; set; }

        public void SetCleanerStatus(CleanerStatus status)
        {
            Status = status;
            if(status == CleanerStatus.Banned)
            {
                _scheduleEntries.Clear();
            }
        }

        public void UpdateOrderFilter(OrderFilter orderFilter)
        {
            MaxMessLevel = orderFilter.MaxMessLevel;
            MinPrice = orderFilter.MinPrice;
            MinClientRating = orderFilter.MinClientRating;
        }

        public void UpdateSchedule(IEnumerable<ScheduleEntry> scheduleEntries)
        {
            _scheduleEntries.Clear();
            _scheduleEntries.AddRange(scheduleEntries);
        }

        public OrderFilter GetOrderFilter()
        {
            return new OrderFilter(MaxMessLevel, MinClientRating, MinPrice);
        }
    }
}

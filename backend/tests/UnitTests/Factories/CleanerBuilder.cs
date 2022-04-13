using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;

namespace UnitTests.Factories
{
    public class CleanerBuilder
    {
        private Cleaner _cleaner;
        public string cleanerId = "123";
        public MessLevel maxMessLevel = MessLevel.Huge;
        public decimal minPrice = 100;
        public int minClientRating = 4;
        public CleanerStatus status = CleanerStatus.Active;
        public List<ScheduleEntry> scheduleEntries = new()
        { new(TimeOnly.MinValue, TimeOnly.MaxValue, DayOfWeek.Tuesday) };

        public CleanerBuilder()
        {
            _cleaner = GetWithDefaultValues();
        }

        private Cleaner GetWithDefaultValues()
        {
            var orderFilter = new OrderFilter(maxMessLevel, minClientRating, minPrice);
            var cleaner = new Cleaner(cleanerId, status, scheduleEntries, orderFilter);
            return cleaner;
        }

        public Cleaner Build()
        {
            return _cleaner;
        }

        public void WithCleanerId(string id)
        {
            _cleaner.CleanerId = id;
        }

        public void WithStatus(CleanerStatus status)
        {
            _cleaner.SetCleanerStatus(status);
        }

        public void WithOrderFilter(OrderFilter orderFilter)
        {
            _cleaner.UpdateOrderFilter(orderFilter);
        }

        public void WithSchedule(IEnumerable<ScheduleEntry> scheduleEntries)
        {
            _cleaner.UpdateSchedule(scheduleEntries);
        }
    }
}

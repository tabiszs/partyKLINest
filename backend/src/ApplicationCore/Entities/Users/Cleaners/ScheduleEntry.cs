using System;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record ScheduleEntry
    {
        private ScheduleEntry()
        {

        }

        public ScheduleEntry(TimeOnly start, TimeOnly end, DayOfWeek dayOfWeek)
        {
            Start = start;
            End = end;
            DayOfWeek = dayOfWeek;
        }

        public long ScheduleEntryId { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }

        public DayOfWeek DayOfWeek { get; private set; }
    }
}

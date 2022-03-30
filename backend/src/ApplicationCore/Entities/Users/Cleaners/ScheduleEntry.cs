using System;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record ScheduleEntry
    {
        public long ScheduleEntryId { get; set; }

        public long CleanerId { get; set; }
        public Cleaner Cleaner { get; set; }

        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

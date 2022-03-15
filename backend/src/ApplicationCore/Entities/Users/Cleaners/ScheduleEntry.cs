using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public class ScheduleEntry
    {
        public long ScheduleEntryId { get; set; }

        public long CleanerId { get; set; }
        public Cleaner Cleaner { get; set; }

        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}

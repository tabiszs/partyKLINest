using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public class Cleaner
    {
        public long CleanerId { get; set; }

        public List<ScheduleEntry> ScheduleEntries { get; set; }

        public CleanerStatus Status { get; set; }

        public MessLevel MaxMessLevel { get; set; }
        public decimal MinPrice { get; set; }
        public int MinClientRating { get; set; }

        // for now we leave localization, it will be solved as a future task
    }
}

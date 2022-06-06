using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;

namespace PartyKlinest.ApplicationCore.Specifications
{
    public class CleanerWithSchedule : Specification<Cleaner>
    {
        public CleanerWithSchedule(string cleanerId)
        {
            Query
                .Include(c => c.ScheduleEntries)
                .Where(c => c.CleanerId == cleanerId);
        }
    }
}

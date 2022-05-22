using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Mapper
{
    public static class CleanerMapper
    {
        public static CleanerInfoDTO GetCleanerInfoDTO(Cleaner cleaner)
        {
            List<ScheduleEntryDTO> schedulesDTO = new();
            foreach (var entry in cleaner.ScheduleEntries)
            {
                schedulesDTO.Add(
                    new ScheduleEntryDTO(
                        entry.DayOfWeek,
                        entry.Start.ToShortTimeString(),
                        entry.End.ToShortTimeString()));
            }

            var cleanerDTO = new CleanerInfoDTO(
                schedulesDTO.ToArray(),
                cleaner.OrderFilter.MaxMessLevel,
                cleaner.OrderFilter.MinClientRating,
                cleaner.OrderFilter.MinPrice,
                0,
                cleaner.Status);

            return cleanerDTO;
        }

        public static Cleaner GetCleaner(string cleanerId, CleanerInfoDTO cleanerInfo)
        {
            List<ScheduleEntry> scheduleEntries = new();
            foreach (var entry in cleanerInfo.ScheduleEntries)
            {
                var ts = TimeOnly.Parse(entry.Start);
                var te = TimeOnly.Parse(entry.End);
                scheduleEntries.Add(new ScheduleEntry(ts, te, entry.DayOfWeek));
            }
            var filter = new OrderFilter(cleanerInfo.MaxMess, cleanerInfo.MinClientRating, cleanerInfo.MinPrice);
            var status = cleanerInfo.Status;
            var cleaner = new Cleaner(cleanerId, status, scheduleEntries, filter);

            return cleaner;
        }

        public static List<CleanerInfoDTO> GetCleanerInfoDTOs(IEnumerable<Cleaner> cleaners)
        {
            return cleaners.Select(x => GetCleanerInfoDTO(x)).ToList();
        }
    }
}

using PartyKlinest.ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    /// <summary>
    /// Cleaner information neccessary to match orders.
    /// </summary>
    public record CleanerInfoDTO
    {
        public CleanerInfoDTO(ScheduleEntryDTO[] scheduleEntries,
            MessLevel maxMess, int minClientRating, decimal minPrice,
            float maxLocationRange, string status)
        {
            ScheduleEntries = scheduleEntries;
            MaxMess = maxMess;
            MinClientRating = minClientRating;
            MinPrice = minPrice;
            MaxLocationRange = maxLocationRange;
            Status = status;
        }

        [Required]
        public ScheduleEntryDTO[] ScheduleEntries { get; init; }

        public MessLevel MaxMess { get; init; }

        public int MinClientRating { get; init; }

        public decimal MinPrice { get; init; }

        public float MaxLocationRange { get; init; }

        public string Status { get; init; }

    }
}
